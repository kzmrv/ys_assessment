using System;
using System.Collections.Generic;
using GroceryStore.Implementation.Domain;
using GroceryStore.Implementation.Infrastructure;
using GroceryStore.Implementation.Repository.ProductRepository;
using GroceryStore.Implementation.Repository.ShoppingRepository;
using GroceryStore.Implementation.Service.PriceCalculators;
using GroceryStore.Implementation.Service.Terminals;
using Ninject;

namespace GroceryStore.Implementation.Service
{

    /// <summary>
    /// Defines a basic class for working with shop. 
    /// Allows adding products to the product database as well as 
    /// working with the sale terminal.
    /// </summary>
    public class ShopSession
    {
        // A service to provide product database.
        private IProductRepository productRepository;
        // A service to provide database of shoppings.
        private IShoppingRepository shoppingRepository;
        // A service to provide price calculations
        private IPriceCalculator calculator;
        private int productIdCounter;
        /// <summary>
        /// Creates a new session which will use the given services.
        /// </summary>
        internal ShopSession(IProductRepository productRepository, 
            IShoppingRepository shoppingRepository,
            IPriceCalculator calculator)
        {
            this.productRepository = productRepository;
            this.shoppingRepository = shoppingRepository;
            this.calculator = calculator;
        }

        /// <summary>
        /// Returns an instance of shop session with default parameters.
        /// </summary>
        public static ShopSession GetDefaultInstance()
        {
            return DefaultServiceProvider.GetShopSession();
        }

        /// <summary>
        /// Adds products which do not have wholesale price.
        /// </summary>
        public void AddProducts(Dictionary<string, double> products)
        {
            foreach (var product in products)
            {
                Product newProduct = new Product(productIdCounter++,
                    product.Key, product.Value);
                productRepository.AddProduct(newProduct);
            }
        }

        /// <summary>
        /// Adds products which do or do not have wholesale price
        /// </summary>
        /// <param name="products">Dictionary where key is the name of product 
        /// and the value is an array of price, and (if present) wholesale
        ///  amount and price</param>
        public void AddProducts(Dictionary<string, double[]> products)
        {
            foreach (var product in products)
            {
                // Retreive numbers to be parsed.
                double[] args = product.Value;
                Product newProduct;
                // Singlesale case
                if (args.Length == 1)
                {
                    newProduct = new Product(productIdCounter++, product.Key,
                        args[0]);
                }
                else
                {
                    // Wholesale case
                    if (args.Length == 3)
                    {
                        newProduct = new Product(productIdCounter++,
                            product.Key, args[0], args[1], args[2]);
                    }
                    // Unknown case.
                    else throw new ArgumentException("Illegal number of arguments");
                }
                productRepository.AddProduct(newProduct);
            }
        }

        /// <summary>
        /// Returns a new sale terminal with default configuration to work with.
        /// </summary>
        public IPointOfSaleTerminal GetSaleTerminal()
        {
            return new SimplePointOfSaleTerminal(shoppingRepository, 
                productRepository, calculator);
        } 
    }
}
