using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineShoppApp.Data;
using OnlineShoppApp.Models;
using OnlineShoppApp.Service;
using System;

namespace OnlineShoppApp
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                       .AddDbContext<ShoppingContext>()
                       .AddTransient<ICustomerRepository,CustomerRepository>()
                       .AddTransient<IProductRepository,ProductRepository>()
                       .AddTransient<IOrderRepository, OrderRepository>()
                       .AddTransient<IOrderLineRepository, OrderLineRepository>()
                       .AddTransient<IProductService, ProductService>()
                       .AddTransient<IOrderService, OrderService>()
                       .AddTransient<IOrderLineService, OrderLineService>()
                       .AddTransient<ICustomerService, CustomerService>()
                       .AddTransient<IShoppingCartService, ShoppingCartService>()
                       .BuildServiceProvider();

            using (var context = serviceProvider.GetService<ShoppingContext>())
            {
                context?.Database.Migrate();
            }

            using (var scope = serviceProvider.CreateScope())
            {
                var customerService = scope.ServiceProvider.GetRequiredService<ICustomerService>();
                var productService = scope.ServiceProvider.GetRequiredService<IProductService>();
                var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();
                var orderLineService = scope.ServiceProvider.GetRequiredService<IOrderLineService>();
                var shoppingCartService = scope.ServiceProvider.GetService<IShoppingCartService>();

                Dictionary<int, List<CartItem>> CartItems = new();

                // Start The app
                Console.WriteLine("========================");
                Console.WriteLine("City-450 Shopping App");
                Console.WriteLine("========================\n");

                while (true)
                {
                    Menu();
                    try
                    {
                        Console.Write(": ");
                        var input = Console.ReadLine()?.Trim().ToLower();
                        List<CartItem> cartItems = null;
                        switch (input)
                        {
                            case "x":
                                return;
                            case "a":
                                await customerService.CreateCustomer(InputCustomerInfo());
                                break;
                            case "b":
                                await productService.CreateAsync(InputProductInfo());
                                break;
                            case "c":
                                await customerService.ListCustomersAsync();
                                break;
                            case "d":
                                await productService.ListProductsAsync();
                                break;
                            case "e":
                                await orderService.ShowOrdersAsync();
                                break;
                            case "f":
                                cartItems = await CheckCartInfo(CartItems, customerService);
                                if (cartItems != null)
                                    await shoppingCartService.Checkout(cartItems);
                                break;
                            case "g":
                                Console.Write("\nEnter customer Id: ");
                                input = Console.ReadLine()?.Trim().ToLower();
                                if(int.TryParse(input, out int customerId))
                                {
                                    await customerService.ShowCustomerAsync(customerId);
                                }else
                                {
                                    Console.WriteLine("\nCustomer Id must be an integer!\n");
                                }
                                break;
                            case "h":
                                Console.Write("\nEnter product Id: ");
                                input = Console.ReadLine()?.Trim().ToLower();
                                if (int.TryParse(input, out int productId))
                                {
                                    await productService.DisplayProductAsync(productId);
                                }
                                else
                                {
                                    Console.WriteLine("\nProduct Id must be an integer!\n");
                                }
                                break;
                            case "i":
                               var cart = await InputCartnfo(customerService, productService);
                                if (cart== null)
                                {
                                    Console.WriteLine("Item not added to shopping cart!\n");
                                }
                                else
                                {
                                   shoppingCartService.AddToCart(CartItems, cart);
                                }
                                break;
                            case "j":
                                cartItems = await CheckCartInfo(CartItems, customerService);
                                if (cartItems != null)
                                    shoppingCartService.ShowCartItems(cartItems);
                                break;
                        }
                    }
                    catch (Exception e)
                    {

                        Console.WriteLine($"\n{e}\n");
                    }
                }
            }
        }

        private async static Task<List<CartItem>> CheckCartInfo(Dictionary<int, List<CartItem>> CartItems, ICustomerService customerService)
        {
            Console.Write("Enter customer Id: ");
            if (!int.TryParse(Console.ReadLine(), out int customerId))
            {
                Console.WriteLine("Invalid input for customer Id. Please enter a valid number.");
                return null;
            }

            try
            {
                Customer customer = await customerService.GetCustomerAsync(customerId);

                if (customer == null)
                {
                    Console.WriteLine($"\nCustomer with ID {customerId} not found!\n");
                    return null;
                }

                if (CartItems.TryGetValue(customerId, out List<CartItem> cartItems))
                {
                    return cartItems;
                }
                else
                {
                    Console.WriteLine($"\nNo items found in the cart for customer {customer.Name} (ID: {customerId}).\n");
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An unexpected error occurred: {e.Message}\n");
                return null;
            }
        }


        private static async Task<CartItem> InputCartnfo(ICustomerService customerService, IProductService productService)
        {
            Console.Write("Enter product Id: ");
            if (!int.TryParse(Console.ReadLine(), out int productId))
            {
                Console.WriteLine("Invalid input for product. Please enter a valid number.");
            }

            Console.Write("Enter customer Id: ");
            if (!int.TryParse(Console.ReadLine(), out int customerId))
            {
                Console.WriteLine("Invalid input for product. Please enter a valid number.");
            }

            Console.Write("Enter quantity: ");
            // Use decimal.TryParse to safely convert the input to decimal
            if (!int.TryParse(Console.ReadLine(), out int quantity))
            {
                Console.WriteLine("Invalid input for qunatity. Please enter a valid integer.");
            }

            Product product = null;
            Customer customer = null;
            try
            {
                product = await  productService.GetProductAsync(productId);
                if (product == null) 
                {
                    Console.WriteLine($"\nProduct with id: {productId} not found!\n");
                    return null;
                }
                customer = await customerService.GetCustomerAsync(customerId);
                if (customer == null)
                {
                    Console.WriteLine($"\nCustomer with id: {customerId} not found!\n");
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unexpected error occured: {e.Message}\n");
            }
            // Create a new Product object with the entered data
            CartItem cartItem = new()
            {
                ProductId = productId,
                ProductName = product.Name,
                CustomerId = customer.Id,
                Price = product.Price,
                Quantity = quantity,
            };
            return cartItem;
        }

        static Customer InputCustomerInfo()
        {
            Console.Write("Enter customer name: ");
            string name = Console.ReadLine();

            Console.Write("Enter customer address: ");
            string address = Console.ReadLine();

            Console.Write("Enter customer telephone: ");
            string telephone = Console.ReadLine();

            Console.Write("Enter customer email: ");
            string email = Console.ReadLine();

            // Create a new Customer object with the entered data
            Customer newCustomer = new Customer
            {
                Name = name,
                Address = address,
                Telephone = telephone,
                Email = email
            };
            return newCustomer;
        }

        static Product InputProductInfo()
        {
            Console.Write("Enter product name: ");
            string name = Console.ReadLine();

            Console.Write("Enter product category: ");
            string category = Console.ReadLine();

            Console.Write("Enter product description: ");
            string desc = Console.ReadLine();

            Console.Write("Enter product unit price: ");
            // Use decimal.TryParse to safely convert the input to decimal
            if (!decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                Console.WriteLine("Invalid input for price. Please enter a valid decimal.");
                return null; // You might want to handle this case appropriately in your application
            }

            Console.Write("Enter product discount if none enter zero(0): ");
            // Use decimal.TryParse to safely convert the input to decimal
            if (!decimal.TryParse(Console.ReadLine(), out decimal discount))
            {
                Console.WriteLine("Invalid input for discount. Please enter a valid decimal.");
                return null; // You might want to handle this case appropriately in your application
            }

            // Create a new Product object with the entered data
            Product newProduct = new Product
            {
                Name = name,
                Category = category,
                Description = desc,
                Price = price,
                Discount = discount
            };
            return newProduct;
        }

        static void Menu()
        {
            Console.WriteLine("a - Create Customer");
            Console.WriteLine("b - Create Product");
            Console.WriteLine("c - Browse Customers");
            Console.WriteLine("d - Browse Products");
            Console.WriteLine("e - Browse Orders");
            Console.WriteLine("f - Checkout");
            Console.WriteLine("g - Find Customer");
            Console.WriteLine("h - Find Product");
            Console.WriteLine("i - Add To Cart");
            Console.WriteLine("j - View Shopping Cart");
            Console.WriteLine("x - Exit");
        }
    }
}
