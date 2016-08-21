using System;
using System.Collections.Generic;
using GroceryStore.Implementation.Service;

namespace GroceryStoreCustomTests
{

    // Custom examples
    class Examples
    {
        static void Main(string[] args)
        {
            Example1();
            Console.ReadKey(true);
        }

        static void Example1()
        {
            Dictionary<string, double[]> products = new Dictionary<string, double[]>
            {
                ["A"] = new[] { 1.25, 3.0, 3.0 },
                ["B"] = new[] { 4.25 },
                ["C"] = new[] { 1.0, 6, 5 },
                ["D"] = new[] { 0.75 }
            };
            var session = ShopSession.GetDefaultInstance();
            session.AddProducts(products);
            Console.WriteLine("Custom example: calculating prices:");
            var terminal = session.GetSaleTerminal();
            // ABCDABA - 1st example in brochure
            terminal.Scan("A", "B", "C", "D", "A", "B", "A");
            Console.WriteLine("Total price of shopping 1 is " +
                              $"{terminal.SaveAndCalculatePrice()}");
            // CCCCCCC
            terminal.Scan("C", "C", "C", "C", "C", "C", "C");
            Console.WriteLine("Total price of shopping 2 is " +
                              $"{terminal.SaveAndCalculatePrice()}");
            // ABCD
            terminal.Scan("A", "B", "C", "D");
            Console.WriteLine("Total price of shopping 3 is " +
                             $"{terminal.SaveAndCalculatePrice()}");
        }
    }
}
