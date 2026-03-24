namespace SalesManagementAPI.DTOs
{
    // ── Request DTOs ──────────────────────────────────────────────────────────

    public class CreateCustomerDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
    }

    public class UpdateCustomerDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string RevenueTier { get; set; } = string.Empty;
    }

    public class CustomerFilterDto
    {
        public string? Name { get; set; }
        public string? Region { get; set; }
        public string? RevenueTier { get; set; }
        public string? Email { get; set; }

        // Pagination
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }

    public class CustomerSegmentDto
    {
        // Filter by region
        public string? Region { get; set; }

        // Filter by revenue tier: Low | Medium | High
        public string? RevenueTier { get; set; }

        // Filter by minimum number of orders
        public int? MinOrders { get; set; }

        // AND | OR
        public string Logic { get; set; } = "AND";
    }

    public class AddNoteDto
    {
        public string Note { get; set; } = string.Empty;

        // Optional reminder date
        public DateTime? ReminderDate { get; set; }
    }

    public class MergeCustomerDto
    {
        // The customer to keep
        public int PrimaryCustomerId { get; set; }

        // The customer to merge into primary (will be archived)
        public int SecondaryCustomerId { get; set; }
    }

    // ── Response DTOs ─────────────────────────────────────────────────────────

    public class CustomerDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string RevenueTier { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int CreatedByUserId { get; set; }
    }

    public class CustomerDetailDto
    {
        public CustomerDto Customer { get; set; } = null!;
        public List<CustomerNoteDto> Notes { get; set; } = new();
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
    }

    public class CustomerNoteDto
    {
        public int Id { get; set; }
        public string Note { get; set; } = string.Empty;
        public DateTime? ReminderDate { get; set; }
        public bool ReminderSent { get; set; }
        public string CreatedByName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }

    public class PagedCustomerDto
    {
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public List<CustomerDto> Customers { get; set; } = new();
    }
}
