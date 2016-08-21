using GroceryStore.Implementation.Domain;

namespace GroceryStore.Implementation.Service.PriceCalculators
{
    /// <summary>
    /// Defines a price calculator interface for shopping instances
    /// as well as for partial shopping units.
    /// </summary>
    interface IPriceCalculator
    {
        /// <summary>
        /// Returns the summary price of the whole shopping instance.
        /// </summary>
        double CalculateTotalPrice(SingleShopping shopping);
        /// <summary>
        /// Returns the price of single shopping unit(defined only by single product 
        /// and its amount).
        /// </summary>
        double CalculateUnitPrice(ShoppingUnit shoppingUnit);
    }
}
