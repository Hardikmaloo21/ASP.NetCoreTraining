namespace SalesManagementAPI.Models
{
    public class SalesOrderItem
    {
        public int Id { get; set; }

        public int SalesOrderId { get; set; }
        public SalesOrder SalesOrder { get; set; } = null!;

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public string ProductName { get; set; } = string.Empty;
        public string ProductSKU { get; set; } = string.Empty;

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

        // Return tracking
        public int ReturnedQuantity { get; set; } = 0;
    }
}
