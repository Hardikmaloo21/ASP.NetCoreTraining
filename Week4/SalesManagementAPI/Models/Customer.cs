namespace SalesManagementAPI.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;

        // Revenue tier: Low | Medium | High
        public string RevenueTier { get; set; } = "Low";
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Who created this customer
        public int CreatedByUserId { get; set; }

        // Navigation property
        public ICollection<CustomerNote> Notes { get; set; } = new List<CustomerNote>();
    }
}