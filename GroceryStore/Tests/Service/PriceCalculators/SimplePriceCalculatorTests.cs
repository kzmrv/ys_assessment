using GroceryStore.Implementation.Domain;
using GroceryStore.Implementation.Service.PriceCalculators;
using NUnit.Framework;

namespace GroceryStore.Tests.Service.PriceCalculators
{

    class SimplePriceCalculatorTests
    {
        double tolerance = 0.00001;

        [Test]
        public void CalculateUnitPrice_SinglepriceTest()
        {
            var calculator = new SimplePriceCalculator();
            var product = new Product(1, "a", 0.11);
            var unit = new ShoppingUnit(10, product);
            var price = calculator.CalculateUnitPrice(unit);
            var expectedPrice = 1.1;
            Assert.AreEqual(price, expectedPrice, tolerance);
        }

        [Test]
        public void CalculateUnitPrice_WholesalepriceTest()
        {
            var calculator = new SimplePriceCalculator();
            var product = new Product(1, "a", 10, 10, 1);
            var unit = new ShoppingUnit(10, product);
            var price = calculator.CalculateUnitPrice(unit);
            var expectedPrice = 1;
            Assert.AreEqual(expectedPrice, price, tolerance);
        }

        [Test]
        public void CalculateUnitPrice_WholesaleSinglepriceTest()
        {
            var calculator = new SimplePriceCalculator();
            var product = new Product(1, "a", 10, 10, 1);
            var unit = new ShoppingUnit(9, product);
            var price = calculator.CalculateUnitPrice(unit);
            var expectedPrice = 90;
            Assert.AreEqual(expectedPrice, price, tolerance);
        }

        [Test]
        public void CalculateTotalPrice_SingleunitTest()
        {
            var calculator = new SimplePriceCalculator();
            var product = new Product(1, "a", 10, 10, 1);
            var unit = new ShoppingUnit(5, product);
            var shopping = new SingleShopping(1);
            shopping.Add(unit);
            var price = calculator.CalculateTotalPrice(shopping);
            var expectedPrice = 50;
            var tolerance = 0.00001;
            Assert.AreEqual(expectedPrice, price, tolerance);
        }

        [Test]
        public void CalculateTotalPrice_CombinedTest()
        {
            var calculator = new SimplePriceCalculator();
            var productS = new Product(1, "a", 10);
            var productW = new Product(2, "b", 20, 10, 80);
            var productWS = new Product(3, "c", 20, 20, 300);
            var unitS = new ShoppingUnit(5, productS);
            var unitW = new ShoppingUnit(10, productW);
            var unitWS = new ShoppingUnit(15, productWS);
            var shopping = new SingleShopping(1);
            shopping.Add(unitS);
            var tolerance = 0.00001;
            var price = calculator.CalculateTotalPrice(shopping);
            var expectedPrice = 50;
            Assert.AreEqual(expectedPrice, price, tolerance);
            shopping.Add(unitW);
            price = calculator.CalculateTotalPrice(shopping);
            expectedPrice = 130;
            Assert.AreEqual(expectedPrice, price, tolerance);
            shopping.Add(unitWS);
            price = calculator.CalculateTotalPrice(shopping);
            expectedPrice = 430;
            Assert.AreEqual(expectedPrice, price, tolerance);
        }
    }
}
