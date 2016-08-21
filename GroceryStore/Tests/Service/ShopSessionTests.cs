using System;
using System.Collections.Generic;
using GroceryStore.Implementation.Domain;
using GroceryStore.Implementation.Repository.ProductRepository;
using GroceryStore.Implementation.Repository.ShoppingRepository;
using GroceryStore.Implementation.Service;
using GroceryStore.Implementation.Service.PriceCalculators;
using Moq;
using NUnit.Framework;

namespace GroceryStore.Tests.Service
{
    class ShopSessionTests
    {
        [Test]
        public void GetSalesTerminalTest()
        {
            var instance = new ShopSession(new InMemoryProductRepository(),
                new InMemoryShoppingRepository(),
                new SimplePriceCalculator());
            Assert.NotNull(instance.GetSaleTerminal());
        }

        [Test]
        public void AddProductsTest()
        {
            var products = new Mock<IProductRepository>();
            var shops = new Mock<IShoppingRepository>();
            var calculator = new Mock<IPriceCalculator>();
            var session = new ShopSession(products.Object,
                shops.Object, calculator.Object);
            session.AddProducts(new Dictionary<string, double>
            {
                ["b"] = 1.2
            });
            session.AddProducts(new Dictionary<string, double[]>
            {
                ["a"] = new[] { 1.0 },
                ["c"] = new[] { 1.2, 2, 2}
            });
            products.Verify(x => x.AddProduct(
                It.IsAny<Product>()), Times.Exactly(3));
        }

        [Test]
        public void AddProductsTest_IllegalArguments()
        {
            var products = new Mock<IProductRepository>();
            var shops = new Mock<IShoppingRepository>();
            var calculator = new Mock<IPriceCalculator>();
            var session = new ShopSession(products.Object,
                shops.Object, calculator.Object);
            Assert.Throws<ArgumentException>(() =>
                session.AddProducts(new Dictionary<string, double[]>
                {
                    ["a"] = new[] { 1.0, 2, 3, 4, 5 }
                }));

        }

        [Test]
        public void GetDefaultInstance_NotNullTest()
        {
            Assert.NotNull(ShopSession.GetDefaultInstance());
        }
    }
}
