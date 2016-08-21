#Grocery store
### Description
The project contains methods for managing a grocery store allowing to store products and process shoppings. 
Its architecture is logically separated by different units. The project itself contains no databases, but its architecture allows replacing in-memory storages with any other kinds of storages in flexible way. All business logic services can be replaced.
### Classes
**Product** defines a basic class to store products.

**ShoppingUnit** defines a part of shopping instance (product-amount-price)

**SingleShopping** defines a shopping instance, contains id, total price and storage of shopping units.

**DefaultServiceProvider** is a module to provide dependency injection for internal project usage.

**InMemoryShoppingStorage** and **InMemoryProductStorage** provide storages to contain shoppings and products respectively.

**SimplePointOfSaleTerminal** is a class to control the shopping process, add namings, save shopping instances etc.

**ShopSession** defines a self constructing session of shop workday. Contains all storages, business logic classes.
## Public API:
1) **ShopSession** class defines a shop session instance to add products and provide a sale terminal.

2) **IPointOfSaleTerminal** interface contains methods to manage the shoppings.
## Packages
**NUnit** and **Moq** for testing and **Ninject** for dependency injection and inner IoC container(for inner usage only). 
No external dependencies are required in user-project to use project functionality.
## Examples
Examples project is a console application to test main project manually. There is built-in example from task with document.
## Tests
Unit tests provided inside the main project. Total coverage of the business logic is 100%, checked by NUnit. Domain elements, which are not part of program logic are not provided with unit tests due to their triviality.
## What to do?
Launch GroceryStoreCustomTests project. Check out unit tests in GroceryStore project.

