namespace SalesManagementAPI.DTOs
{
    // ── Request DTOs ──────────────────────────────────────────────────────────

    public class CreateOrderItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class CreateOrderDto
    {
        public int CustomerId { get; set; }
        public decimal DiscountPercent { get; set; } = 0;
        public string Notes { get; set; } = string.Empty;
        public List<CreateOrderItemDto> Items { get; set; } = new();
    }

    public class OrderFilterDto
    {
        public string? Status { get; set; }
        public int? SalesRepId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        // Pagination
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }

    public class ReturnItemDto
    {
        public int ProductId { get; set; }
        public int ReturnQuantity { get; set; }
    }

    public class ProcessReturnDto
    {
        public string Reason { get; set; } = string.Empty;
        public List<ReturnItemDto> Items { get; set; } = new();
    }

    public class ApproveDiscountDto
    {
        public string Notes { get; set; } = string.Empty;
    }

    // ── Response DTOs ─────────────────────────────────────────────────────────

    public class OrderItemDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductSKU { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public int ReturnedQuantity { get; set; }
    }

    public class OrderDto
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public int SalesRepId { get; set; }
        public string SalesRepName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public decimal SubTotal { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Tax { get; set; }
        public decimal TotalAmount { get; set; }
        public string DiscountStatus { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<OrderItemDto> Items { get; set; } = new();
    }

    public class PagedOrderDto
    {
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public List<OrderDto> Orders { get; set; } = new();
    }
}
