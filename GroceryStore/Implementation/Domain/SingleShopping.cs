using System;
using System.Collections;
using System.Collections.Generic;
using GroceryStore.Implementation.Service.PriceCalculators;

namespace GroceryStore.Implementation.Domain
{

    /// <summary>
    /// Defines a class to store single-customer shopping. 
    /// Contains shopping units(product-amount-price relationship).
    /// </summary>
    class SingleShopping : IEnumerable<ShoppingUnit>
    {
        // Stores all the shopping units in this instance.
        private List<ShoppingUnit> units = new List<ShoppingUnit>();
        private double price;

        public SingleShopping(int id)
        {
            this.Id = id;
        }

        public int Id { get; private set; }

        /// <summary>
        /// Read-only property which returns the price of shopping.
        /// Throws InvalidOperationException if shopping is not calculated.
        /// </summary>
        public double Price
        {
            get
            {
                if (isCalculated) return price;
                throw new InvalidOperationException("Price is not calculated");
            }
        }

        /// <summary>
        /// Defines whether the shopping instance has been calculated.
        /// </summary>
        public bool isCalculated { get; private set; }

        /// <summary>
        /// Adds a shopping unit to the current instance.
        /// </summary>
        public void Add(ShoppingUnit unit)
        {
            units.Add(unit);
        }

        /// <summary>
        /// Removes a shopping unit from the current instance.
        /// </summary>
        public void Remove(ShoppingUnit unit)
        {
            units.Remove(unit);
        }

        /// <summary>
        /// Sets the shopping price, evaluated by given calculator.
        /// </summary>
        public void Calculate(IPriceCalculator calculator)
        {
            price = calculator.CalculateTotalPrice(this);
            isCalculated = true;
        }

        /// <summary>
        /// Returns true if no shopping unit present in the shopping instance.
        /// </summary>
        public bool IsEmpty()
        {
            return units.Count == 0;
        }

        /// <summary>
        /// Gets the enumerator, containing shopping units.
        /// </summary>
        public IEnumerator<ShoppingUnit> GetEnumerator()
        {
            return units.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
