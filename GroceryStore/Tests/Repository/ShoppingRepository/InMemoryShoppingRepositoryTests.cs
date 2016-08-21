using GroceryStore.Implementation.Domain;
using GroceryStore.Implementation.Repository.ShoppingRepository;
using NUnit.Framework;

namespace GroceryStore.Tests.Repository.ShoppingRepository
{
    class InMemoryShoppingRepositoryTests
    {
        [Test]
        public void CreateNewShoppingTest()
        {
            var instance = new InMemoryShoppingRepository();
            var shopping1 = instance.CreateNewShopping();
            var shopping2 = instance.CreateNewShopping();
            Assert.AreNotEqual(shopping1.Id, shopping2.Id);
            Assert.AreEqual(false, shopping1.GetEnumerator().MoveNext());
            Assert.AreEqual(false, shopping2.GetEnumerator().MoveNext());
        }

        [Test]
        public void AddGetShoppingTest()
        {
            var instance = new InMemoryShoppingRepository();
            var shopping1 = instance.CreateNewShopping();
            var shopping2 = instance.CreateNewShopping();
            shopping1.Add(new ShoppingUnit(1, new Product(1, "1", 1)));
            shopping2.Add(new ShoppingUnit(2, new Product(2, "2", 2)));
            instance.SaveShopping(shopping1);
            instance.SaveShopping(shopping2);
            Assert.AreEqual(shopping1, instance.GetShoppingById(0));
            Assert.AreEqual(shopping2, instance.GetShoppingById(1));
            Assert.IsNull(instance.GetShoppingById(2));
        }
    }
}
