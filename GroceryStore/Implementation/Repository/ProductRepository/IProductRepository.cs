using GroceryStore.Implementation.Domain;

namespace GroceryStore.Implementation.Repository.ProductRepository
{
    /// <summary>
    /// A service to manage products. Allows adding them to the storage and 
    /// retreiving back.
    /// </summary>
    interface IProductRepository
    {

        /// <summary>
        /// Adds a product to the storage.
        /// </summary>
        void AddProduct(Product product);

        /// <summary>
        /// Retreives a product from the storage by its name.
        /// </summary>
        Product GetProductByName(string name);
    }
}
