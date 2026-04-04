using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Bill
    {
        [Key]
        public int Id { get; set; }

        public string InvoiceNumber { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public decimal Subtotal { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal TaxAmount { get; set; }

        public decimal GrandTotal { get; set; }

        public List<BillItem> BillItems { get; set; } = new();
    }
}