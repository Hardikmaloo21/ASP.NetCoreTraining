using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SalesManagementAPI.Data;
using SalesManagementAPI.Models;
using SalesManagementAPI.DTOs;

namespace SalesManagementAPI.Controllers
{
    [ApiController]
    [Route("api/products")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        // ── US-2.1: Add new product ───────────────────────────────────────────
        // POST api/products
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult CreateProduct(CreateProductDto dto)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (userIdClaim == null)
                return Unauthorized();

            // Validate required fields
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest(new { message = "Product name is required." });

            if (string.IsNullOrWhiteSpace(dto.SKU))
                return BadRequest(new { message = "SKU is required." });

            if (string.IsNullOrWhiteSpace(dto.Category))
                return BadRequest(new { message = "Category is required." });

            if (dto.Price <= 0)
                return BadRequest(new { message = "Price must be greater than zero." });

            // Check duplicate SKU
            var existingSKU = _context.Products.FirstOrDefault(p => p.SKU == dto.SKU);
            if (existingSKU != null)
                return Conflict(new { message = $"A product with SKU '{dto.SKU}' already exists." });

            var product = new Product
            {
                Name = dto.Name,
                SKU = dto.SKU,
                Category = dto.Category,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity,
                Description = dto.Description,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                CreatedByUserId = int.Parse(userIdClaim)
            };

            _context.Products.Add(product);

            _context.AuditLogs.Add(new AuditLog
            {
                ActingUserId = int.Parse(userIdClaim),
                Action = "PRODUCT_CREATED",
                Details = $"Product '{product.Name}' (SKU: {product.SKU}) created.",
                CreatedAt = DateTime.UtcNow
            });

            _context.SaveChanges();

            return Ok(MapToDto(product));
        }

        // ── US-2.2: Edit existing product ─────────────────────────────────────
        // PUT api/products/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult UpdateProduct(int id, UpdateProductDto dto)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (userIdClaim == null)
                return Unauthorized();

            var product = _context.Products.Find(id);

            if (product == null)
                return NotFound(new { message = "Product not found." });

            if (dto.Price <= 0)
                return BadRequest(new { message = "Price must be greater than zero." });

            // Track price change for audit
            var priceChanged = product.Price != dto.Price;
            var oldPrice = product.Price;

            product.Name = dto.Name;
            product.Category = dto.Category;
            product.Price = dto.Price;
            product.StockQuantity = dto.StockQuantity;
            product.Description = dto.Description;
            product.UpdatedAt = DateTime.UtcNow;

            _context.AuditLogs.Add(new AuditLog
            {
                ActingUserId = int.Parse(userIdClaim),
                Action = "PRODUCT_UPDATED",
                Details = priceChanged
                    ? $"Product '{product.Name}' updated. Price changed from {oldPrice:C} to {dto.Price:C}."
                    : $"Product '{product.Name}' updated.",
                CreatedAt = DateTime.UtcNow
            });

            _context.SaveChanges();

            return Ok(MapToDto(product));
        }

        // ── US-2.3: Deactivate product ────────────────────────────────────────
        // PUT api/products/deactivate/{id}
        [HttpPut("deactivate/{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult DeactivateProduct(int id)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (userIdClaim == null)
                return Unauthorized();

            var product = _context.Products.Find(id);

            if (product == null)
                return NotFound(new { message = "Product not found." });

            if (!product.IsActive)
                return BadRequest(new { message = "Product is already deactivated." });

            product.IsActive = false;
            product.UpdatedAt = DateTime.UtcNow;

            _context.AuditLogs.Add(new AuditLog
            {
                ActingUserId = int.Parse(userIdClaim),
                Action = "PRODUCT_DEACTIVATED",
                Details = $"Product '{product.Name}' (SKU: {product.SKU}) deactivated.",
                CreatedAt = DateTime.UtcNow
            });

            _context.SaveChanges();

            return Ok(new { message = $"Product '{product.Name}' has been deactivated." });
        }

        // ── US-2.3: Reactivate product ────────────────────────────────────────
        // PUT api/products/activate/{id}
        [HttpPut("activate/{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult ActivateProduct(int id)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (userIdClaim == null)
                return Unauthorized();

            var product = _context.Products.Find(id);

            if (product == null)
                return NotFound(new { message = "Product not found." });

            if (product.IsActive)
                return BadRequest(new { message = "Product is already active." });

            product.IsActive = true;
            product.UpdatedAt = DateTime.UtcNow;

            _context.AuditLogs.Add(new AuditLog
            {
                ActingUserId = int.Parse(userIdClaim),
                Action = "PRODUCT_ACTIVATED",
                Details = $"Product '{product.Name}' (SKU: {product.SKU}) reactivated.",
                CreatedAt = DateTime.UtcNow
            });

            _context.SaveChanges();

            return Ok(new { message = $"Product '{product.Name}' has been reactivated." });
        }

        // ── US-2.4: Search & filter products ─────────────────────────────────
        // GET api/products?name=&category=&minPrice=&maxPrice=&isActive=&page=1&pageSize=20
        [HttpGet]
        public IActionResult GetProducts([FromQuery] ProductFilterDto filter)
        {
            var query = _context.Products.AsQueryable();

            // Filter by name
            if (!string.IsNullOrWhiteSpace(filter.Name))
                query = query.Where(p => p.Name.Contains(filter.Name));

            // Filter by category
            if (!string.IsNullOrWhiteSpace(filter.Category))
                query = query.Where(p => p.Category == filter.Category);

            // Filter by price range
            if (filter.MinPrice.HasValue)
                query = query.Where(p => p.Price >= filter.MinPrice.Value);

            if (filter.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= filter.MaxPrice.Value);

            // Filter by active status
            // Default: show only active products
            if (filter.IsActive.HasValue)
                query = query.Where(p => p.IsActive == filter.IsActive.Value);
            else
                query = query.Where(p => p.IsActive == true);

            // Total count before pagination
            var totalCount = query.Count();

            // Pagination
            var products = query
                .OrderByDescending(p => p.CreatedAt)
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(p => MapToDto(p))
                .ToList();

            return Ok(new PagedProductDto
            {
                TotalCount = totalCount,
                Page = filter.Page,
                PageSize = filter.PageSize,
                TotalPages = (int)Math.Ceiling((double)totalCount / filter.PageSize),
                Products = products
            });
        }

        // ── US-2.4: Get single product ────────────────────────────────────────
        // GET api/products/{id}
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
                return NotFound(new { message = "Product not found." });

            return Ok(MapToDto(product));
        }

        // ── US-2.5: Bulk CSV Import ───────────────────────────────────────────
        // POST api/products/import
        // CSV format: Name,SKU,Category,Price,StockQuantity,Description
        [HttpPost("import")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> ImportProducts(IFormFile file)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (userIdClaim == null)
                return Unauthorized();

            if (file == null || file.Length == 0)
                return BadRequest(new { message = "Please upload a CSV file." });

            if (!file.FileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
                return BadRequest(new { message = "Only CSV files are accepted." });

            if (file.Length > 5 * 1024 * 1024)
                return BadRequest(new { message = "File size must not exceed 5MB." });

            var result = new ImportResultDto();
            var lines = new List<string>();

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                string? line;
                while ((line = await reader.ReadLineAsync()) != null)
                    lines.Add(line);
            }

            if (lines.Count <= 1)
                return BadRequest(new { message = "CSV file is empty or only contains headers." });

            // Skip header row
            for (int i = 1; i < lines.Count; i++)
            {
                var columns = lines[i].Split(',');

                if (columns.Length < 6)
                {
                    result.FailedCount++;
                    result.Errors.Add($"Row {i + 1}: Invalid format. Expected 6 columns (Name,SKU,Category,Price,StockQuantity,Description).");
                    continue;
                }

                var name = columns[0].Trim();
                var sku = columns[1].Trim();
                var category = columns[2].Trim();
                var priceStr = columns[3].Trim();
                var stockStr = columns[4].Trim();
                var description = columns[5].Trim();

                // Validate required fields
                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(sku) || string.IsNullOrWhiteSpace(category))
                {
                    result.FailedCount++;
                    result.Errors.Add($"Row {i + 1}: Name, SKU and Category are required.");
                    continue;
                }

                if (!decimal.TryParse(priceStr, out decimal price) || price <= 0)
                {
                    result.FailedCount++;
                    result.Errors.Add($"Row {i + 1}: Invalid price '{priceStr}'.");
                    continue;
                }

                if (!int.TryParse(stockStr, out int stock) || stock < 0)
                {
                    result.FailedCount++;
                    result.Errors.Add($"Row {i + 1}: Invalid stock quantity '{stockStr}'.");
                    continue;
                }

                // Check duplicate SKU
                var exists = _context.Products.Any(p => p.SKU == sku);
                if (exists)
                {
                    result.FailedCount++;
                    result.Errors.Add($"Row {i + 1}: SKU '{sku}' already exists.");
                    continue;
                }

                var product = new Product
                {
                    Name = name,
                    SKU = sku,
                    Category = category,
                    Price = price,
                    StockQuantity = stock,
                    Description = description,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedByUserId = int.Parse(userIdClaim)
                };

                _context.Products.Add(product);
                result.SuccessCount++;
            }

            _context.AuditLogs.Add(new AuditLog
            {
                ActingUserId = int.Parse(userIdClaim),
                Action = "PRODUCT_BULK_IMPORT",
                Details = $"Bulk import: {result.SuccessCount} succeeded, {result.FailedCount} failed.",
                CreatedAt = DateTime.UtcNow
            });

            _context.SaveChanges();

            return Ok(result);
        }

        // ── Helper: Map Product to ProductDto ─────────────────────────────────
        private static ProductDto MapToDto(Product p) => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            SKU = p.SKU,
            Category = p.Category,
            Price = p.Price,
            StockQuantity = p.StockQuantity,
            Description = p.Description,
            IsActive = p.IsActive,
            CreatedAt = p.CreatedAt,
            UpdatedAt = p.UpdatedAt,
            CreatedByUserId = p.CreatedByUserId
        };
    }
}
