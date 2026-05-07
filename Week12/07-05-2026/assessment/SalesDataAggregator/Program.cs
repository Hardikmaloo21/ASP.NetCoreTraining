using System;
using System.Collections.Generic;
using System.Linq;

class SalesDataAggregator
{
    // Sales record class
    class Sale
    {
        public string ProductId;
        public string Region;
        public double Amount;

        public Sale(string productId, string region, double amount)
        {
            ProductId = productId;
            Region = region;
            Amount = amount;
        }
    }

    static void Main()
    {
        // Sample sales data
        List<Sale> sales = new List<Sale>
        {
            new Sale("P001", "North", 1500),
            new Sale("P001", "South", 2000),
            new Sale("P002", "North", 3000),
            new Sale("P001", "East", 2500),
            new Sale("P002", "South", 1800),
            new Sale("P003", "North", 1200),
            new Sale("P001", "West", 2200),
            new Sale("P002", "West", 2800),
            new Sale("P003", "South", 900),
            new Sale("P002", "East", 3200)
        };

        double threshold = 2000;

        Console.WriteLine("--- Sales Report by Product and Region ---\n");

        // Group sales by product
        var productGroups = sales.GroupBy(s => s.ProductId);

        Dictionary<string, double> productAverages =
            new Dictionary<string, double>();

        foreach (var productGroup in productGroups)
        {
            Console.WriteLine("Product " + productGroup.Key + ":\n");

            foreach (var sale in productGroup)
            {
                Console.WriteLine(
                    "  " + sale.Region +
                    ": $" + sale.Amount
                );
            }

            double total = productGroup.Sum(s => s.Amount);
            double average = productGroup.Average(s => s.Amount);
            double min = productGroup.Min(s => s.Amount);
            double max = productGroup.Max(s => s.Amount);

            productAverages[productGroup.Key] = average;

            Console.WriteLine(
                "\n  Total: $" + total +
                ", Average: $" + average.ToString("0.00")
            );

            Console.WriteLine();
        }

        // Best selling product by region
        Console.WriteLine("--- Best Selling Product by Region ---\n");

        var regionGroups = sales.GroupBy(s => s.Region);

        foreach (var regionGroup in regionGroups)
        {
            var bestSale = regionGroup
                .OrderByDescending(s => s.Amount)
                .First();

            Console.WriteLine(
                regionGroup.Key + ": " +
                bestSale.ProductId +
                " ($" + bestSale.Amount + ")"
            );
        }

        // Underperforming products
        Console.WriteLine(
            "\n--- Underperforming Products (< $" +
            threshold + " average) ---\n"
        );

        foreach (var product in productAverages)
        {
            if (product.Value < threshold)
            {
                Console.WriteLine(
                    product.Key +
                    " ($" + product.Value.ToString("0.00") + ")"
                );
            }
        }
    }
}