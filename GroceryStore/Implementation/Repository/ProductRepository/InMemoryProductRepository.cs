using System;
using System.Collections.Generic;
using GroceryStore.Implementation.Domain;

namespace GroceryStore.Implementation.Repository.ProductRepository
{
    /// <summary>
    /// Provides an in-RAM storage to add and retrieve products.
    /// </summary>
    class InMemoryProductRepository : IProductRepository
    {
        private List<Product> storage = new List<Product>();

        /// <summary>
        /// Adds a product to the in-memory storage.
        /// Throws argument exception if product with the same name is already present.
        /// </summary>
        public void AddProduct(Product product)
        {
            if (GetProductByName(product.Name) != null)
            {
                throw new ArgumentException("Storage already contains " +
                                            $"product with name '{product.Name}' ");
            }
            storage.Add(product);
        }

        /// <summary>
        /// Retreives the product from an in-memory storage by its name.
        /// Returns null if product was not found.
        /// </summary>
        public Product GetProductByName(string name)
        {
            foreach (Product product in storage)
            {
                if (product.Name == name) return product;
            }
            return null;
        }
    }
}
