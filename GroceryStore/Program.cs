using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Castle.Core.Internal;
using GroceryStore.Implementation.Infrastructure;
using GroceryStore.Implementation.Domain;
using GroceryStore.Implementation.Service;
using Ninject;


namespace GroceryStore
{
    // Example of inner usage of service provider(ninject).
    // Switch to the console application to make this work
    class Program
    {
        private static IKernel appKernel;
        static void Main(string[] args)
        {
            appKernel = new StandardKernel(new DefaultServiceProvider());
            var session = appKernel.Get<ShopSession>();

            session.AddProducts(new Dictionary<string, double[]>() { ["A"] = new[] { 1.0, 5, 2 } });
            var terminal = session.GetSaleTerminal();
            terminal.Scan("A", 10);
            terminal.Scan("A");
            Console.WriteLine(terminal.SaveAndCalculatePrice());
            Console.ReadKey(true); 
        }
    }
}
