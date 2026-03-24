using System;

namespace consoleApp
{
    class Product
    {
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int Quantity { get; set; }
        public string Brand { get; set; }
        public bool ISO { get; set; }

        // Constructor
        public Product(string name, int id, DateTime expiryDate, int quantity, string brand, bool isISOStandard)
        {
            ProductName = name;
            ProductId = id;
            ExpiryDate = expiryDate;
            Quantity = quantity;
            Brand = brand;
            ISO = isISOStandard;
        }

        public void DisplayProductData()
        {
            Console.WriteLine($"Product Name: {ProductName}");
            Console.WriteLine($"Product Id: {ProductId}");
            Console.WriteLine($"Brand Name: {Brand}");
            Console.WriteLine($"Quantity: {Quantity}");
            Console.WriteLine($"Expiry Date: {ExpiryDate}");
            Console.WriteLine($"ISO standard: {ISO}");
        }
    }

    class Program
    {
        static void Main()
        {
            Product prod = new Product(
                "Marlboro",
                65464,
                new DateTime(2028, 12, 31),
                20,
                "Cigarette",
                true
            );

            prod.DisplayProductData();
        }
    }
}