namespace SalesManagementAPI.Models
{
    public class SalesOrder
    {
        public int Id { get; set; }

        // Unique order number e.g. ORD-20260314-001
        public string OrderNumber { get; set; } = string.Empty;

        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        public int SalesRepId { get; set; }
        public User SalesRep { get; set; } = null!;

        // Status: Pending | Completed | Cancelled | Returned
        public string Status { get; set; } = "Pending";

        public decimal SubTotal { get; set; }
        public decimal DiscountPercent { get; set; } = 0;
        public decimal DiscountAmount { get; set; } = 0;
        public decimal Tax { get; set; } = 0;
        public decimal TotalAmount { get; set; }

        // Discount approval
        // DiscountStatus: None | PendingApproval | Approved | Rejected
        public string DiscountStatus { get; set; } = "None";
        public int? ApprovedByUserId { get; set; }

        public string Notes { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        public ICollection<SalesOrderItem> Items { get; set; } = new List<SalesOrderItem>();
    }
}