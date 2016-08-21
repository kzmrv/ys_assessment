using GroceryStore.Implementation.Domain;

namespace GroceryStore.Implementation.Service.PriceCalculators
{
    /// <summary>
    /// Provides simple price calculations for single and wholesale shoppings.
    /// </summary>
    class SimplePriceCalculator : IPriceCalculator
    {
        /// <summary>
        /// Calculates the total price of shopping as the sum of prices of its units.
        /// </summary>
        public double CalculateTotalPrice(SingleShopping shopping)
        {
            double result = 0.0;
            foreach (var unit in shopping)
            {
                unit.CalculatePrice(this);
                result += unit.Price;
            }
            return result;
        }

        /// <summary>
        /// Calculates the price of shopping unit using the wholesale prices where
        ///  possible.
        /// </summary>
        public double CalculateUnitPrice(ShoppingUnit shoppingUnit)
        {
            Product product = shoppingUnit.Product;
            double amount = shoppingUnit.Amount;
            if (product.WholesaleAmount == 0)
            {
                return calculateSinglePrice(product, amount);
            }
            int wholesaleTimes = (int)(amount / product.WholesaleAmount);
            double restAmount = amount - wholesaleTimes * product.WholesaleAmount;
            double resultPrice = calculateWholesalePrice(product, wholesaleTimes)
                + calculateSinglePrice(product, restAmount);
            return resultPrice;
        }

        /// <summary>
        /// Calculates the price of given amount of given product using its 
        /// single-amount price.
        /// </summary>
        private double calculateSinglePrice(Product product, double amount)
        {
            return product.SinglePrice * amount;
        }

        /// <summary>
        /// Calculates the price of given times of wholesale amount of given product.
        /// </summary>
        private double calculateWholesalePrice(Product product, int wholesaleTimes)
        {
            return wholesaleTimes * product.WholesalePrice;
        }
    }
}