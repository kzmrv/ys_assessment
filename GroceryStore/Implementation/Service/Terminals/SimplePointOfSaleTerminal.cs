using System;
using System.Collections.Generic;
using System.Linq;
using GroceryStore.Implementation.Domain;
using GroceryStore.Implementation.Repository.ProductRepository;
using GroceryStore.Implementation.Repository.ShoppingRepository;
using GroceryStore.Implementation.Service.PriceCalculators;

namespace GroceryStore.Implementation.Service.Terminals
{
    /// <summary>
    /// Defines a casual terminal with common methods to control the shopping process. 
    /// </summary>
    class SimplePointOfSaleTerminal : IPointOfSaleTerminal
    {
        // Services to be used with current terminal.
        private IShoppingRepository shoppingRepository;
        private IProductRepository productRepository;
        private IPriceCalculator calculator;
        // A shopping, this terminal is currently working with.
        private SingleShopping currentShopping;


        public SimplePointOfSaleTerminal(IShoppingRepository shoppingRepository,
            IProductRepository productRepository, IPriceCalculator calculator)
        {
            this.shoppingRepository = shoppingRepository;
            this.productRepository = productRepository;
            this.calculator = calculator;
            currentShopping = shoppingRepository.CreateNewShopping();
        }

        /// <summary>
        /// Add a given amount of the product to the current shopping by its 
        /// given name.
        /// </summary>
        public void Scan(string productName, double amount)
        {
            Product product = productRepository.GetProductByName(productName);
            if (product == null)
            {
                throw new ArgumentException("Unknown product");
            }
            // Check if we already have this product in the shopping.
            foreach (var unit in currentShopping)
            {
                if (unit.Product == product)
                {
                    var uAmount = unit.Amount;
                    var uProduct = unit.Product;
                    currentShopping.Remove(unit);
                    currentShopping.Add(new ShoppingUnit(uAmount + amount, uProduct));
                    return;
                }
            }
            currentShopping.Add(new ShoppingUnit(amount, product));
        }

        /// <summary>
        /// Add a single amount of each product to the current shopping by given 
        /// product names.
        /// </summary>
        public void Scan(params string[] productNames)
        {
            foreach (var productName in productNames)
            {
                Scan(productName, 1.0);
            }
        }

        /// <summary>
        /// Add a single amount of the product to the current shopping by its 
        /// given name.
        /// </summary>
        public void Scan(string productName)
        {
            Scan(productName, 1.0);
        }

        /// <summary>
        /// Return the price of the current shopping,
        /// save it and create a new one. 
        /// Returns zero if shopping is empty.
        /// </summary>
        public double SaveAndCalculatePrice()
        {
            // No need to save shopping without any unit.
            if (currentShopping.IsEmpty())
            {
                return 0;
            }
            // Else calculate, save and switch to the new shopping.
            double price = calculator.CalculateTotalPrice(currentShopping);
            shoppingRepository.SaveShopping(currentShopping);
            currentShopping = shoppingRepository.CreateNewShopping();
            return price;
        }

        /// <summary>
        /// Returns list which contains current shoppings(only copies).
        /// </summary>
        public List<ShoppingUnit> GetCurrentShoppingList()
        {
            List<ShoppingUnit> result = new List<ShoppingUnit>();
            foreach (var shopping in currentShopping)
            {
                result.Add(shopping);
            }
            return result;

        }
    }
}
