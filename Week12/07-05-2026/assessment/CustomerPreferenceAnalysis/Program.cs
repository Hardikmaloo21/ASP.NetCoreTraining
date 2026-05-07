using System;
using System.Collections.Generic;
using System.Linq;

class CustomerPreferenceAnalysis
{
    static void Main()
    {
        HashSet<string> electronics = new HashSet<string>
        {
            "C001", "C002", "C003", "C005", "C008"
        };
        HashSet<string> clothing = new HashSet<string>
        {
            "C002", "C004", "C005", "C006", "C009"
        };
        HashSet<string> books = new HashSet<string>
        {
            "C003", "C005", "C007", "C008", "C010"
        };
        Console.WriteLine("--- Customer Preference Analysis ---\n");

        // 1. Customers who bought from ANY category (Union)
        HashSet<string> anyCategory = new HashSet<string>(electronics);
        anyCategory.UnionWith(clothing);
        anyCategory.UnionWith(books);
        Console.WriteLine("1. Customers in ANY category (Union):");
        Console.WriteLine(string.Join(", ", anyCategory.OrderBy(x => x)));
        Console.WriteLine("Total: " + anyCategory.Count + " customers\n");

        // 2. Customers who bought from ALL categories (Intersection)
        HashSet<string> allCategories = new HashSet<string>(electronics);
        allCategories.IntersectWith(clothing);
        allCategories.IntersectWith(books);
        Console.WriteLine("2. Customers in ALL categories (Intersection):");
        Console.WriteLine(string.Join(", ", allCategories.OrderBy(x => x)));
        Console.WriteLine("Total: " + allCategories.Count + " customer(s)\n");

        // 3. Customers ONLY in Electronics
        HashSet<string> onlyElectronics = new HashSet<string>(electronics);
        HashSet<string> otherCategories = new HashSet<string>(clothing);
        otherCategories.UnionWith(books);
        onlyElectronics.ExceptWith(otherCategories);
        Console.WriteLine("3. Customers ONLY in Electronics (Difference):");
        Console.WriteLine(string.Join(", ", onlyElectronics.OrderBy(x => x)));
        Console.WriteLine("Total: " + onlyElectronics.Count + " customers\n");

        // 4. Customers in Electronics AND Books but NOT Clothing
        HashSet<string> electronicsAndBooks = new HashSet<string>(electronics);
        electronicsAndBooks.IntersectWith(books);
        electronicsAndBooks.ExceptWith(clothing);
        Console.WriteLine("4. Customers in Electronics AND Books but NOT Clothing:");
        Console.WriteLine(string.Join(", ", electronicsAndBooks.OrderBy(x => x)));
        Console.WriteLine("Total: " + electronicsAndBooks.Count + " customers");
    }
}