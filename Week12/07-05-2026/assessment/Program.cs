using System;
using System.Collections.Generic;

class Program
{
    public string ProductId;
    public int quantity;
    Dictionary<string,int> stock = new Dictionary<string,int>();
    public void AddStock(string ProductId, int quantity)
    {
        this.ProductId = ProductId;
        this.quantity = quantity;

        if(!stock.ContainsKey(ProductId))
        {
            stock[ProductId] = quantity;
        }
        else
        {
            stock[ProductId] += quantity;
        }
    }

    public void RemoveStock(string ProductId, int quantity)
    {
        this.ProductId = ProductId;
        this.quantity = quantity;

        if(stock.ContainsKey(ProductId))
        {
            if(stock[ProductId] >= quantity)
            {
                stock[ProductId] -= quantity;
            }
            else
            {
                Console.WriteLine("Not enough stock to remove.");
            }
        }
        else
        {
            Console.WriteLine("Product not found in stock.");
        }
    }

    public void CheckStock(string ProductId)
    {
        if(stock.ContainsKey(ProductId))
        {
            Console.WriteLine($"Product ID: {ProductId}, Quantity: {stock[ProductId]}");
        }
        else
        {
            Console.WriteLine("Product not found in stock.");
        }
    }

    public void BulkUpdateStock(Dictionary<string,int> updates)
    {
        foreach(var update in updates)
        {
            if(stock.ContainsKey(update.Key))
            {
                stock[update.Key] += update.Value;
            }
            else
            {
                stock[update.Key] = update.Value;
            }
        }
    }

    public void DisplayStock()
    {
        Console.WriteLine("Current Stock:");
        foreach(var item in stock)
        {
            Console.WriteLine($"Product ID: {item.Key}, Quantity: {item.Value}");
        }
    }

    public static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine());
        Program inventory = new Program();
        for(int i = 0; i < n; i++)
        {
            string[] input = Console.ReadLine().Split(' ');
            string command = input[0];
            string productId = input[1];
            int quantity = int.Parse(input[2]);

            switch(command)
            {
                case "ADD":
                    inventory.AddStock(productId, quantity);
                    break;
                case "REMOVE":
                    inventory.RemoveStock(productId, quantity);
                    break;
                case "CHECK":
                    inventory.CheckStock(productId);
                    break;
                case "BULK":
                    Dictionary<string,int> updates = new Dictionary<string,int>();
                    for(int j = 1; j < input.Length; j += 2)
                    {
                        string id = input[j];
                        int qty = int.Parse(input[j + 1]);
                        updates[id] = qty;
                    }
                    inventory.BulkUpdateStock(updates);
                    break;
                case "DISPLAY":
                    inventory.DisplayStock();
                    break;
                default:
                    Console.WriteLine("Invalid command.");
                    break;
            }
        }
    }

}