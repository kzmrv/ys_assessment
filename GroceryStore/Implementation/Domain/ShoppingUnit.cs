using System;
using GroceryStore.Implementation.Service.PriceCalculators;

namespace GroceryStore.Implementation.Domain
{
    /// <summary>
    /// Defines a class to store single shopping unit properties, such as amount, 
    /// product reference and the calculated price.
    /// </summary>
    struct ShoppingUnit
    {
        // Defines, whether the unit price has been calculated.
        private bool isCalculated;
        private double price;

        /// <summary>
        /// Creates a new shopping unit with given product and amount.
        /// </summary>
        public ShoppingUnit(double amount, Product product)
        {
            Amount = amount;
            Product = product;
            isCalculated = false;
            price = 0;
        }

        /// <summary>
        /// Returns the calculated unit price.
        /// Throws IllegalOperationException if not calculated.
        /// </summary>
        public double Price
        {
            get
            {
                if (isCalculated) return price;
                throw new InvalidOperationException("Price is not calculated");
            }
        }

        public double Amount { get; set; }
        public Product Product { get; set; }

        /// <summary>
        /// Calculates the shopping unit price with given calculator.
        /// </summary>
        public void CalculatePrice(IPriceCalculator calculator)
        {
            price = calculator.CalculateUnitPrice(this);
            isCalculated = true;
        }
    }
}
