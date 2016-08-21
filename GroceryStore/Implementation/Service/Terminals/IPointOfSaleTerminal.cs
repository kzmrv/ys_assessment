namespace GroceryStore.Implementation.Service.Terminals
{
    /// <summary>
    /// A terminal to provide basic shopping functions.
    /// Allows to add namings and calculate total price.
    /// </summary>
    public interface IPointOfSaleTerminal
    {
        /// <summary>
        /// Adds a given amount of given product to the shopping.
        /// Throws IllegalArgumentException if the product is unknown.
        /// </summary>
        void Scan(string productName, double amount);

        /// <summary>
        /// Adds a single amount of given products to the shoping.
        /// Throws IllegalArgumentException if any product is unknown.
        /// </summary>
        void Scan(params string[] productName);

        /// <summary>
        /// Saves the current shopping and returns its price.
        /// </summary>
        double SaveAndCalculatePrice();
    }
}
