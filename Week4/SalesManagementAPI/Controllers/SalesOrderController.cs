using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SalesManagementAPI.Data;
using SalesManagementAPI.Models;
using SalesManagementAPI.DTOs;
using Microsoft.EntityFrameworkCore;

namespace SalesManagementAPI.Controllers
{
    [ApiController]
    [Route("api/orders")]
    [Authorize]
    public class SalesOrderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SalesOrderController(AppDbContext context)
        {
            _context = context;
        }

        // ── US-4.1: Create new sales order ────────────────────────────────────
        // POST api/orders
        [HttpPost]
        public IActionResult CreateOrder(CreateOrderDto dto)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim);

            // Validate customer exists
            var customer = _context.Customers.Find(dto.CustomerId);
            if (customer == null)
                return NotFound(new { message = "Customer not found." });

            // Validate items
            if (dto.Items == null || dto.Items.Count == 0)
                return BadRequest(new { message = "Order must have at least one item." });

            // Validate discount
            if (dto.DiscountPercent < 0 || dto.DiscountPercent > 100)
                return BadRequest(new { message = "Discount must be between 0 and 100." });

            // Build order items and calculate subtotal
            var orderItems = new List<SalesOrderItem>();
            decimal subTotal = 0;

            foreach (var itemDto in dto.Items)
            {
                var product = _context.Products.Find(itemDto.ProductId);

                if (product == null)
                    return NotFound(new { message = $"Product ID {itemDto.ProductId} not found." });

                if (!product.IsActive)
                    return BadRequest(new { message = $"Product '{product.Name}' is inactive and cannot be ordered." });

                if (itemDto.Quantity <= 0)
                    return BadRequest(new { message = $"Quantity for product '{product.Name}' must be greater than zero." });

                if (product.StockQuantity < itemDto.Quantity)
                    return BadRequest(new { message = $"Insufficient stock for '{product.Name}'. Available: {product.StockQuantity}." });

                var unitPrice = itemDto.UnitPrice > 0 ? itemDto.UnitPrice : product.Price;
                var totalPrice = unitPrice * itemDto.Quantity;
                subTotal += totalPrice;

                orderItems.Add(new SalesOrderItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    ProductSKU = product.SKU,
                    Quantity = itemDto.Quantity,
                    UnitPrice = unitPrice,
                    TotalPrice = totalPrice,
                    ReturnedQuantity = 0
                });

                // Deduct stock
                product.StockQuantity -= itemDto.Quantity;
            }

            // Calculate discount
            // Discount > 10% requires manager approval
            var discountAmount = subTotal * (dto.DiscountPercent / 100);
            var discountStatus = "None";

            if (dto.DiscountPercent > 0 && dto.DiscountPercent <= 10)
                discountStatus = "Approved";
            else if (dto.DiscountPercent > 10)
                discountStatus = "PendingApproval";

            // Calculate tax (18% GST)
            var taxableAmount = subTotal - discountAmount;
            var tax = taxableAmount * 0.18m;
            var totalAmount = taxableAmount + tax;

            // Generate unique order number
            var orderNumber = $"ORD-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString("N")[..6].ToUpper()}";

            var order = new SalesOrder
            {
                OrderNumber = orderNumber,
                CustomerId = dto.CustomerId,
                SalesRepId = userId,
                Status = discountStatus == "PendingApproval" ? "Pending" : "Completed",
                SubTotal = subTotal,
                DiscountPercent = dto.DiscountPercent,
                DiscountAmount = discountAmount,
                Tax = tax,
                TotalAmount = totalAmount,
                DiscountStatus = discountStatus,
                Notes = dto.Notes,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Items = orderItems
            };

            _context.SalesOrders.Add(order);

            _context.AuditLogs.Add(new AuditLog
            {
                ActingUserId = userId,
                Action = "ORDER_CREATED",
                Details = $"Order '{orderNumber}' created for customer '{customer.FullName}'. Total: {totalAmount:C}.",
                CreatedAt = DateTime.UtcNow
            });

            _context.SaveChanges();

            return Ok(MapToDto(order));
        }

        // ── US-4.3: Get all orders (Manager/Admin) ────────────────────────────
        // GET api/orders
        [HttpGet]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult GetAllOrders([FromQuery] OrderFilterDto filter)
        {
            var query = _context.SalesOrders
                .Include(o => o.Customer)
                .Include(o => o.SalesRep)
                .Include(o => o.Items)
                .AsQueryable();

            // Filter by status
            if (!string.IsNullOrWhiteSpace(filter.Status))
                query = query.Where(o => o.Status == filter.Status);

            // Filter by sales rep
            if (filter.SalesRepId.HasValue)
                query = query.Where(o => o.SalesRepId == filter.SalesRepId.Value);

            // Filter by date range
            if (filter.FromDate.HasValue)
                query = query.Where(o => o.CreatedAt >= filter.FromDate.Value);

            if (filter.ToDate.HasValue)
                query = query.Where(o => o.CreatedAt <= filter.ToDate.Value);

            var totalCount = query.Count();

            var orders = query
                .OrderByDescending(o => o.CreatedAt)
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToList()
                .Select(o => MapToDto(o))
                .ToList();

            return Ok(new PagedOrderDto
            {
                TotalCount = totalCount,
                Page = filter.Page,
                PageSize = filter.PageSize,
                TotalPages = (int)Math.Ceiling((double)totalCount / filter.PageSize),
                Orders = orders
            });
        }

        // ── Get single order ──────────────────────────────────────────────────
        // GET api/orders/{id}
        [HttpGet("{id}")]
        public IActionResult GetOrder(int id)
        {
            var order = _context.SalesOrders
                .Include(o => o.Customer)
                .Include(o => o.SalesRep)
                .Include(o => o.Items)
                .FirstOrDefault(o => o.Id == id);

            if (order == null)
                return NotFound(new { message = "Order not found." });

            return Ok(MapToDto(order));
        }

        // ── US-4.4: Get my orders (Sales Rep) ────────────────────────────────
        // GET api/orders/my-orders
        [HttpGet("my-orders")]
        public IActionResult GetMyOrders([FromQuery] OrderFilterDto filter)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim);

            var query = _context.SalesOrders
                .Include(o => o.Customer)
                .Include(o => o.SalesRep)
                .Include(o => o.Items)
                .Where(o => o.SalesRepId == userId)
                .AsQueryable();

            // Filter by status
            if (!string.IsNullOrWhiteSpace(filter.Status))
                query = query.Where(o => o.Status == filter.Status);

            // Filter by date range
            if (filter.FromDate.HasValue)
                query = query.Where(o => o.CreatedAt >= filter.FromDate.Value);

            if (filter.ToDate.HasValue)
                query = query.Where(o => o.CreatedAt <= filter.ToDate.Value);

            var totalCount = query.Count();

            var orders = query
                .OrderByDescending(o => o.CreatedAt)
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToList()
                .Select(o => MapToDto(o))
                .ToList();

            return Ok(new PagedOrderDto
            {
                TotalCount = totalCount,
                Page = filter.Page,
                PageSize = filter.PageSize,
                TotalPages = (int)Math.Ceiling((double)totalCount / filter.PageSize),
                Orders = orders
            });
        }

        // ── US-4.2: Approve discount ──────────────────────────────────────────
        // PUT api/orders/{id}/approve-discount
        [HttpPut("{id}/approve-discount")]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult ApproveDiscount(int id, ApproveDiscountDto dto)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim);

            var order = _context.SalesOrders
                .Include(o => o.Customer)
                .Include(o => o.SalesRep)
                .Include(o => o.Items)
                .FirstOrDefault(o => o.Id == id);

            if (order == null)
                return NotFound(new { message = "Order not found." });

            if (order.DiscountStatus != "PendingApproval")
                return BadRequest(new { message = "This order does not have a pending discount approval." });

            order.DiscountStatus = "Approved";
            order.Status = "Completed";
            order.ApprovedByUserId = userId;
            order.Notes = string.IsNullOrWhiteSpace(dto.Notes)
                ? order.Notes
                : $"{order.Notes} | Approval note: {dto.Notes}";
            order.UpdatedAt = DateTime.UtcNow;

            _context.AuditLogs.Add(new AuditLog
            {
                ActingUserId = userId,
                Action = "DISCOUNT_APPROVED",
                Details = $"Discount of {order.DiscountPercent}% approved for order '{order.OrderNumber}'.",
                CreatedAt = DateTime.UtcNow
            });

            _context.SaveChanges();

            return Ok(MapToDto(order));
        }

        // ── US-4.2: Reject discount ───────────────────────────────────────────
        // PUT api/orders/{id}/reject-discount
        [HttpPut("{id}/reject-discount")]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult RejectDiscount(int id, ApproveDiscountDto dto)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim);

            var order = _context.SalesOrders
                .Include(o => o.Customer)
                .Include(o => o.SalesRep)
                .Include(o => o.Items)
                .FirstOrDefault(o => o.Id == id);

            if (order == null)
                return NotFound(new { message = "Order not found." });

            if (order.DiscountStatus != "PendingApproval")
                return BadRequest(new { message = "This order does not have a pending discount approval." });

            // Recalculate total without discount
            order.DiscountStatus = "Rejected";
            order.DiscountPercent = 0;
            order.DiscountAmount = 0;
            order.Tax = order.SubTotal * 0.18m;
            order.TotalAmount = order.SubTotal + order.Tax;
            order.Status = "Completed";
            order.ApprovedByUserId = userId;
            order.Notes = string.IsNullOrWhiteSpace(dto.Notes)
                ? order.Notes
                : $"{order.Notes} | Rejection note: {dto.Notes}";
            order.UpdatedAt = DateTime.UtcNow;

            _context.AuditLogs.Add(new AuditLog
            {
                ActingUserId = userId,
                Action = "DISCOUNT_REJECTED",
                Details = $"Discount rejected for order '{order.OrderNumber}'. Total recalculated to {order.TotalAmount:C}.",
                CreatedAt = DateTime.UtcNow
            });

            _context.SaveChanges();

            return Ok(MapToDto(order));
        }

        // ── Cancel order ──────────────────────────────────────────────────────
        // PUT api/orders/{id}/cancel
        [HttpPut("{id}/cancel")]
        public IActionResult CancelOrder(int id)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim);

            var order = _context.SalesOrders
                .Include(o => o.Items)
                .FirstOrDefault(o => o.Id == id);

            if (order == null)
                return NotFound(new { message = "Order not found." });

            if (order.Status == "Cancelled")
                return BadRequest(new { message = "Order is already cancelled." });

            if (order.Status == "Returned")
                return BadRequest(new { message = "Cannot cancel a returned order." });

            // Restore stock
            foreach (var item in order.Items)
            {
                var product = _context.Products.Find(item.ProductId);
                if (product != null)
                    product.StockQuantity += item.Quantity;
            }

            order.Status = "Cancelled";
            order.UpdatedAt = DateTime.UtcNow;

            _context.AuditLogs.Add(new AuditLog
            {
                ActingUserId = userId,
                Action = "ORDER_CANCELLED",
                Details = $"Order '{order.OrderNumber}' cancelled. Stock restored.",
                CreatedAt = DateTime.UtcNow
            });

            _context.SaveChanges();

            return Ok(new { message = $"Order '{order.OrderNumber}' has been cancelled and stock restored." });
        }

        // ── US-4.5: Process return ────────────────────────────────────────────
        // POST api/orders/{id}/return
        [HttpPost("{id}/return")]
        public IActionResult ProcessReturn(int id, ProcessReturnDto dto)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim);

            var order = _context.SalesOrders
                .Include(o => o.Items)
                .FirstOrDefault(o => o.Id == id);

            if (order == null)
                return NotFound(new { message = "Order not found." });

            if (order.Status != "Completed")
                return BadRequest(new { message = "Only completed orders can be returned." });

            // Check 30-day return window
            if (order.CreatedAt.AddDays(30) < DateTime.UtcNow)
                return BadRequest(new { message = "Return window of 30 days has expired." });

            if (string.IsNullOrWhiteSpace(dto.Reason))
                return BadRequest(new { message = "Return reason is required." });

            // Process each return item
            foreach (var returnItem in dto.Items)
            {
                var orderItem = order.Items
                    .FirstOrDefault(i => i.ProductId == returnItem.ProductId);

                if (orderItem == null)
                    return NotFound(new { message = $"Product ID {returnItem.ProductId} not found in this order." });

                var availableToReturn = orderItem.Quantity - orderItem.ReturnedQuantity;

                if (returnItem.ReturnQuantity <= 0)
                    return BadRequest(new { message = "Return quantity must be greater than zero." });

                if (returnItem.ReturnQuantity > availableToReturn)
                    return BadRequest(new { message = $"Cannot return more than {availableToReturn} units of '{orderItem.ProductName}'." });

                // Update returned quantity
                orderItem.ReturnedQuantity += returnItem.ReturnQuantity;

                // Restore stock
                var product = _context.Products.Find(returnItem.ProductId);
                if (product != null)
                    product.StockQuantity += returnItem.ReturnQuantity;
            }

            order.Status = "Returned";
            order.UpdatedAt = DateTime.UtcNow;

            _context.AuditLogs.Add(new AuditLog
            {
                ActingUserId = userId,
                Action = "ORDER_RETURNED",
                Details = $"Return processed for order '{order.OrderNumber}'. Reason: {dto.Reason}.",
                CreatedAt = DateTime.UtcNow
            });

            _context.SaveChanges();

            return Ok(new { message = $"Return processed successfully for order '{order.OrderNumber}'." });
        }

        // ── Helper: Map SalesOrder to OrderDto ────────────────────────────────
        private static OrderDto MapToDto(SalesOrder o) => new OrderDto
        {
            Id = o.Id,
            OrderNumber = o.OrderNumber,
            CustomerId = o.CustomerId,
            CustomerName = o.Customer?.FullName ?? string.Empty,
            SalesRepId = o.SalesRepId,
            SalesRepName = o.SalesRep?.FullName ?? string.Empty,
            Status = o.Status,
            SubTotal = o.SubTotal,
            DiscountPercent = o.DiscountPercent,
            DiscountAmount = o.DiscountAmount,
            Tax = o.Tax,
            TotalAmount = o.TotalAmount,
            DiscountStatus = o.DiscountStatus,
            Notes = o.Notes,
            CreatedAt = o.CreatedAt,
            UpdatedAt = o.UpdatedAt,
            Items = o.Items.Select(i => new OrderItemDto
            {
                Id = i.Id,
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                ProductSKU = i.ProductSKU,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                TotalPrice = i.TotalPrice,
                ReturnedQuantity = i.ReturnedQuantity
            }).ToList()
        };
    }
}
