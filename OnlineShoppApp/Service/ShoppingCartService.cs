using Microsoft.EntityFrameworkCore.Migrations.Operations;
using OnlineShoppApp.Models;
using System.Diagnostics;
namespace OnlineShoppApp.Service
{
    
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IOrderLineService _orderLineService;
        private readonly IOrderService _orderService;

        public ShoppingCartService(IOrderService orderService, IOrderLineService orderLineService) 
        {
            _orderLineService = orderLineService;
            _orderService = orderService;
        }

        public void AddToCart(Dictionary<int, List<CartItem>> cartItems, CartItem cartItem)
        {
            var customerId = cartItem.CustomerId;

            if (cartItems.TryGetValue(customerId, out List<CartItem>? cartItemList))
            {
                // Check if the product is already in the cart
                var existingItem = cartItemList.FirstOrDefault(item => item.ProductId == cartItem.ProductId);

                if (existingItem != null)
                {
                    // Product is already in the cart, update the quantity
                    existingItem.Quantity += cartItem.Quantity;
                }
                else
                {
                    // Product is not in the cart, add it to the list
                    cartItemList.Add(cartItem);
                }
            }
            else
            {
                // Customer doesn't have any items in the cart, create a new list
                List<CartItem> items = new List<CartItem> { cartItem };
                cartItems.Add(customerId, items);
            }
            Console.WriteLine($"\nProduct: [{cartItem.ProductName}] added to shopping cart!\n");
        }

        public void ShowCartItems(List<CartItem> cartItems)
        {
            decimal amount = 0.0m;
            foreach (var item in cartItems)
            {
                Console.WriteLine(item);
                amount += item.Amount();
            }
            Console.WriteLine("============================");
            Console.WriteLine($"Shopping Cart Amount: {amount}");
            Console.WriteLine("============================");
            Console.WriteLine();
        }

        public async Task Checkout(List<CartItem> cartItems)
        {
            await Task.Run(async () =>
            {
                // Create Order
                Order order = new()
                {
                    CustomerId = cartItems[0].CustomerId,
                    OrderDate = DateTime.Now,
                    Status = "Shipped",
                    PaymentMethod = "Credit"
                };

                try
                {
                    // Create the order
                    await _orderService.CreateAsync(order);

                    // Retrieve the created order with the generated OrderId
                    var createdOrder = await _orderService.GetOrderByCustomerAndDateAsync(cartItems[0].CustomerId, order.OrderDate);

                    if (createdOrder != null)
                    {
                        // Save Order Items
                        foreach (var item in cartItems)
                        {
                            OrderLine orderLine = new()
                            {
                                ProductId = item.ProductId,
                                OrderId = createdOrder.Id, // Use the retrieved OrderId
                                Quantity = item.Quantity,
                                OrderAmount = item.Amount(),
                            };
                            await _orderLineService.CreateAsync(orderLine);
                        }

                        Console.WriteLine($"\nCheckout successful! Order Id {createdOrder.Id}\n");
                    }
                    else
                    {
                        Console.WriteLine("\nFailed to retrieve the created order.\n");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"An error occurred while checking out: {e.Message}\n");
                }
            });
        }
    }
}
