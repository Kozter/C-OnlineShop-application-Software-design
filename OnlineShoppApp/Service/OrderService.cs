using OnlineShoppApp.Data;
using OnlineShoppApp.Models;
using System.Diagnostics;
namespace OnlineShoppApp.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(Order order)
        {
            try
            {
                if (order == null)
                {
                    Debug.WriteLine("Order object cannot be null");
                    return;
                }
                await _repository.AddAsync(order);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e, $"An error occurred while persisting new order");
            }
        }

        public async Task<Order> GetOrderAsync(int Id)
        {
            try
            {
                return await _repository.GetOrderAsync(Id);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e, $"An error occurred while fetching order id {Id}!");
                return null;
            }
        }
        public async Task ShowOrderAsync(int Id)
        {
            try
            {
                var order = await _repository.GetOrderAsync(Id);
                if (order == null)
                {
                    Debug.WriteLine($"Customer with id {Id} not found!");
                }
                else
                {
                    Console.WriteLine($"Order:: [Id: {order.Id}, Customer: {order.Customer.Name}, Order Date: {order.OrderDate}, Status: {order.Status}]\n");
                    decimal _orderTotal = 0.0m;
                    Console.WriteLine("_____");
                    foreach (var item in order.OrderLines)
                    {
                        Console.WriteLine($"Product:: [Name: {item.Product.Name}, Quantity: {item.Quantity}, Amount: {item.OrderAmount}]\n");
                        _orderTotal += item.OrderAmount;
                    }

                    Console.WriteLine("====================");
                    Console.WriteLine($"Order Total: {_orderTotal}");
                    Console.WriteLine("====================\n");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e, $"An error occurred while getting order with id {Id}");
            }
        }
        public async Task ShowOrdersAsync()
        {
            try
            {
                var orders = await _repository.GetOrdersAsync();
                if (orders == null)
                {
                    Debug.WriteLine($"Orders not available!");
                }
                else
                {
                    foreach (var order in orders)
                    {
                        Console.WriteLine($"Order:: [Id: {order.Id}, Customer: {order.Customer.Name}, Order Date: {order.OrderDate}, Status: {order.Status}]\n");
                        decimal _orderTotal = 0.0m;
                        Console.WriteLine("_____");
                        foreach (var item in order.OrderLines)
                        {
                            Console.WriteLine($"Product:: [Name: {item.Product.Name}, Quantity: {item.Quantity}, Amount: {item.OrderAmount}]\n");
                            _orderTotal += item.OrderAmount;
                        }

                        Console.WriteLine("====================");
                        Console.WriteLine($"Order Total: {_orderTotal}");
                        Console.WriteLine("====================\n");
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e, $"An error occurred while fetching orders");
            }
        }

        public async Task<Order> GetOrderByCustomerAndDateAsync(int customerId, DateTime orderDate)
        {
           return await _repository.GetOrderByCustomerAndDateAsync(customerId,orderDate);
        }
    }
}
