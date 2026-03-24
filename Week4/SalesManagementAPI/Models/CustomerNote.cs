namespace SalesManagementAPI.Models
{
    public class CustomerNote
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        public string Note { get; set; } = string.Empty;

        // Follow-up reminder date (optional)
        public DateTime? ReminderDate { get; set; }

        public bool ReminderSent { get; set; } = false;

        // Who wrote this note
        public int CreatedByUserId { get; set; }
        public string CreatedByName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}