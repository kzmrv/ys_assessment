using System.Runtime.CompilerServices;
using GroceryStore.Implementation.Repository.ProductRepository;
using GroceryStore.Implementation.Repository.ShoppingRepository;
using GroceryStore.Implementation.Service;
using GroceryStore.Implementation.Service.PriceCalculators;
using GroceryStore.Implementation.Service.Terminals;
using Ninject.Modules;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace GroceryStore.Implementation.Infrastructure
{
    /// <summary>
    /// Provides required services to the application components.
    /// </summary>
    class DefaultServiceProvider : NinjectModule
    {
        // Inner assembly bindings.
        public override void Load()
        {
            // Default config for ninject service provider
            Bind<ShopSession>().ToSelf();
            Bind<IPriceCalculator>().To<SimplePriceCalculator>();
            Bind<IProductRepository>().To<InMemoryProductRepository>();
            Bind<IPointOfSaleTerminal>().To<SimplePointOfSaleTerminal>();
            Bind<IShoppingRepository>().To<InMemoryShoppingRepository>();
        }

        // To use out of the assembly.
        public static ShopSession GetShopSession()
        {
            return new ShopSession(new InMemoryProductRepository(), 
                new InMemoryShoppingRepository(), new SimplePriceCalculator());
        }
    }
}
