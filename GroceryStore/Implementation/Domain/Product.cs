namespace GroceryStore.Implementation.Domain
{
    /// <summary>
    /// Defines a basic class to store the product properties.
    /// </summary>
    class Product
    {
        /// <summary>
        /// Create a new product without wholesale price.
        /// </summary>
        public Product(int id, string name, double singlePrice)
        {
            Name = name;
            SinglePrice = singlePrice;
            this.Id = id;
        }

        /// <summary>
        /// Create a new product with wholesale price.
        /// </summary>
        public Product(int id, string name, double singlePrice,
            double wholesaleAmount, double wholesalePrice)
            : this(id, name, singlePrice)
        {
            WholesaleAmount = wholesaleAmount;
            WholesalePrice = wholesalePrice;
        }

        /// <summary>
        /// The amount of product to be sold with wholesale price.
        /// </summary>
        public double WholesaleAmount { get; protected set; }

        /// <summary>
        /// The total price of some amount of product, the amount 
        /// is stored as a WholesaleAmount.
        /// </summary>
        public double WholesalePrice { get; protected set; }
        public int Id { get; private set; }
        public string Name { get; protected set; }
        public double SinglePrice { get; protected set; }
    }
}
