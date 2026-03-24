using System;
namespace Exercise1
{
    class Product
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int Quantity { get; set; }
        public string Brand { get; set; }
        public bool IsISOStandard { get; set; }
        public void GetProduct_Deatils()
        {
            Console.Write("Enter the product name: ");
            Name = Console.ReadLine();

            Console.Write("Enter the product id: ");
            Id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter the product expiry date (yyyy-mm-dd): ");
            ExpiryDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter the product quantity: ");
            Quantity = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter the product brand: ");
            Brand = Console.ReadLine();

            Console.Write("Is the product ISO standard? (true/false): ");
            IsISOStandard = Convert.ToBoolean(Console.ReadLine());

        }

        public void DisplayProductDetails()
        {
            Console.WriteLine($"Product Name: {Name}");
            Console.WriteLine($"Product ID: {Id}");
            Console.WriteLine($"Expiry Date: {ExpiryDate.ToShortDateString()}");
            Console.WriteLine($"Quantity: {Quantity}");
            Console.WriteLine($"Brand: {Brand}");
            Console.WriteLine($"ISO Standard: {IsISOStandard}");
        }

        Product(string name, int id, DateTime expiryDate, int quantity, string brand, bool isISOStandard)
        {
            Name = name;
            Id = id;
            ExpiryDate = expiryDate;
            Quantity = quantity;
            Brand = brand;
            IsISOStandard = isISOStandard;
        }


        public static void Main()
        {
            Product product = new Product("Laptop", 101, new DateTime(2025, 12, 31), 50, "TechBrand", true);
            product.DisplayProductDetails();



        }



    }

}