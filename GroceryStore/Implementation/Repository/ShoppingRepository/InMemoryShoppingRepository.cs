using System.Collections.Generic;
using GroceryStore.Implementation.Domain;

namespace GroceryStore.Implementation.Repository.ShoppingRepository
{
    /// <summary>
    /// Provides saving the shopping instances in an in-RAM storage.
    /// </summary>
    class InMemoryShoppingRepository : IShoppingRepository
    {
        private List<SingleShopping> storage = new List<SingleShopping>();
        // A unique shopping identifier number.
        private int shoppingIdCounter;

        /// <summary>
        /// Provides an empty shopping instance.
        /// </summary>
        public SingleShopping CreateNewShopping()
        {
            return new SingleShopping(shoppingIdCounter++);
        }

        /// <summary>
        /// Saves given shopping instance in the in-memory storage.
        /// </summary>
        public void SaveShopping(SingleShopping shopping)
        {
            storage.Add(shopping);
        }

        /// <summary>
        /// Returns the shopping instance with given id or null if not found.
        /// </summary>
        public SingleShopping GetShoppingById(int id)
        {
            foreach (var shopping in storage)
            {
                if(shopping.Id == id) return shopping;
            }
            return null;
        }
    }
}
