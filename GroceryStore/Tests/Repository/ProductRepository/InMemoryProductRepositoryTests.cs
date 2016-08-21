using System;
using GroceryStore.Implementation.Domain;
using GroceryStore.Implementation.Repository.ProductRepository;
using NUnit.Framework;

namespace GroceryStore.Tests.Repository.ProductRepository
{
    class InMemoryProductRepositoryTests
    {
        [Test]
        public void AddGetProduct_SingleunitTest()
        {
            var repository = new InMemoryProductRepository();
            Product expected = new Product(1, "a", 10);
            repository.AddProduct(expected);
            var actual = repository.GetProductByName("a");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AddGetProduct_MultiunitTest()
        {
            var repository = new InMemoryProductRepository();
            Product expected1 = new Product(1, "a", 10);
            Product expected2 = new Product(2, "b", 10);
            Product expected3 = new Product(3, "c", 10);
            Product expected4 = new Product(4, "d", 10);
            repository.AddProduct(expected1);
            repository.AddProduct(expected2);
            repository.AddProduct(expected3);
            repository.AddProduct(expected4);
            var actual2 = repository.GetProductByName("b");
            var actual4 = repository.GetProductByName("d");
            var actual3 = repository.GetProductByName("c");
            var actual1 = repository.GetProductByName("a");
            Assert.AreEqual(expected1, actual1);
            Assert.AreEqual(expected2, actual2);
            Assert.AreEqual(expected3, actual3);
            Assert.AreEqual(expected4, actual4);
        }

        [Test]
        public void AddGetProduct_SameNameTest()
        {
            var repository = new InMemoryProductRepository();
            Product expected1 = new Product(1, "a", 10);
            Product expected2 = new Product(2, "b", 10);
            Product same1 = new Product(3, "a", 10);
            repository.AddProduct(expected1);
            repository.AddProduct(expected2);
            var actual1 = repository.GetProductByName("a");
            var actual2 = repository.GetProductByName("b");
            var ex = Assert.Throws<ArgumentException>(() =>
                repository.AddProduct(same1));
            Assert.AreEqual(expected1, actual1);
            Assert.AreEqual(expected2, actual2);
        }
    }
}
