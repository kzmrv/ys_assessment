using System;
using System.Collections.Generic;
using GroceryStore.Implementation.Domain;
using GroceryStore.Implementation.Repository.ProductRepository;
using GroceryStore.Implementation.Repository.ShoppingRepository;
using GroceryStore.Implementation.Service.PriceCalculators;
using GroceryStore.Implementation.Service.Terminals;
using Moq;
using NUnit.Framework;

namespace GroceryStore.Tests.Service.Terminals
{
    class SimplePointOfSaleTerminalTests
    {
        [Test]
        public void ScanTest_WrongProduct()
        {
            var products = new InMemoryProductRepository();
            products.AddProduct(new Product(0, "a", 1));
            var instance = new SimplePointOfSaleTerminal(
                new InMemoryShoppingRepository(), products,
                new SimplePriceCalculator());
            var ex = Assert.Throws<ArgumentException>(() =>
                instance.Scan("b"));
        }

        [Test]
        public void ScanTest_SingleShoppingUnit()
        {
            var calculator = new Mock<IPriceCalculator>();
            var shoppingRepos = new Mock<IShoppingRepository>();
            var productRepos = new Mock<IProductRepository>();
            var product = new Product(1, "a", 1.0, 3, 2);
            shoppingRepos.Setup(x => x.CreateNewShopping()).Returns(new
                SingleShopping(1));
            productRepos.Setup(
                x => x.GetProductByName("a")).Returns(product);

            var instance = new SimplePointOfSaleTerminal(
                shoppingRepos.Object, productRepos.Object,
                calculator.Object);

            instance.Scan("a");
            var sList = instance.GetCurrentShoppingList();
            Assert.AreEqual(1, sList.Count);
            ShoppingUnit unit = sList[0];
            Assert.AreEqual(product, unit.Product);
            Assert.AreEqual(1.0, unit.Amount);
        }

        [Test]
        public void ScanTest_MultiShoppingUnit()
        {
            var calculator = new Mock<IPriceCalculator>();
            var shoppingRepos = new Mock<IShoppingRepository>();
            var productRepos = new Mock<IProductRepository>();
            List<Product> products = new List<Product>();
            var product1 = new Product(1, "a", 1.0, 100, 2);
            var product2 = new Product(2, "b", 2, 100, 8);
            var product3 = new Product(3, "c", 5, 100, 20);
            var product4 = new Product(4, "d", 10, 100, 40);
            products.Add(product1);
            products.Add(product2);
            products.Add(product3);
            products.Add(product4);
            productRepos.Setup(
               x => x.GetProductByName(It.IsAny<string>())).Returns((string x) =>
               products[(int)(x[0] - 'a')]);
            shoppingRepos.Setup(x => x.CreateNewShopping()).Returns(new
                SingleShopping(1));
            var instance = new SimplePointOfSaleTerminal(
                shoppingRepos.Object, productRepos.Object,
                calculator.Object);

            instance.Scan("a", "b");
            instance.Scan("a", 10);
            instance.Scan("c");
            instance.Scan("d", 5);
            var sList = instance.GetCurrentShoppingList();
            Assert.AreEqual(4, sList.Count);
            // 'a' moved forward
            ShoppingUnit unit1 = sList[1];
            ShoppingUnit unit2 = sList[0];
            //
            ShoppingUnit unit3 = sList[2];
            ShoppingUnit unit4 = sList[3];
            Assert.AreEqual(product1, unit1.Product);
            Assert.AreEqual(11, unit1.Amount);
            Assert.AreEqual(product2, unit2.Product);
            Assert.AreEqual(1, unit2.Amount);
            Assert.AreEqual(product3, unit3.Product);
            Assert.AreEqual(1.0, unit3.Amount);
            Assert.AreEqual(product4, unit4.Product);
            Assert.AreEqual(5, unit4.Amount);
        }

        [Test]
        public void SaveAndCalculatePrice_EmptyShoppingTest()
        {
            var calculator = new Mock<IPriceCalculator>();
            var shoppingRepos = new Mock<IShoppingRepository>();
            var productRepos = new Mock<IProductRepository>();
            shoppingRepos.Setup(x => x.CreateNewShopping()).Returns(new SingleShopping(1));
            var instance = new SimplePointOfSaleTerminal(
                shoppingRepos.Object, productRepos.Object,
                calculator.Object);
            var actual = instance.SaveAndCalculatePrice();
            Assert.AreEqual(0, actual);
            calculator.Verify(x => x.CalculateTotalPrice(
               It.IsAny<SingleShopping>()), Times.Never);
        }

        [Test]
        public void SaveAndCalculatePrice_ShoppingTest()
        {
            var calculator = new Mock<IPriceCalculator>();
            var shoppingRepos = new Mock<IShoppingRepository>();
            var productRepos = new InMemoryProductRepository();
            productRepos.AddProduct(new Product(1,"a",1));

            shoppingRepos.Setup(x => x.CreateNewShopping()).Returns(new SingleShopping(1));
            var instance = new SimplePointOfSaleTerminal(
                shoppingRepos.Object, productRepos,
                calculator.Object);
            instance.Scan("a");
            var actual = instance.SaveAndCalculatePrice();
            calculator.Verify(x=>x.CalculateTotalPrice(
                It.IsAny<SingleShopping>()),Times.Once);
        }

        [Test]
        public void GetCurrentShoppingListTest()
        {
            var products = new InMemoryProductRepository();
            products.AddProduct(new Product(0, "a", 1));
            var instance = new SimplePointOfSaleTerminal(
                new InMemoryShoppingRepository(), products,
                new SimplePriceCalculator());
            Assert.IsEmpty(instance.GetCurrentShoppingList());
            instance.Scan("a");
            Assert.IsNotEmpty(instance.GetCurrentShoppingList());

        }
    }
}
