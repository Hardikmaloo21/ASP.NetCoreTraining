using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class BillItem
    {
        [Key]
        public int Id { get; set; }

        public string ItemName { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string Category { get; set; } = string.Empty;

        [ForeignKey("Bill")]
        public int BillId { get; set; }

        public Bill? Bill { get; set; }
    }
}