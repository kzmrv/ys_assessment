using GroceryStore.Implementation.Domain;

namespace GroceryStore.Implementation.Repository.ShoppingRepository
{
    /// <summary>
    /// A service to manage shopping instances with ability to create and save them. 
    /// </summary>
    interface IShoppingRepository
    {
        /// <summary>
        /// Provides a new shopping instance.
        /// </summary>
        SingleShopping CreateNewShopping();
        
        /// <summary>
        /// Saves given instance.
        /// </summary>
        void SaveShopping(SingleShopping shopping);
    }
}
