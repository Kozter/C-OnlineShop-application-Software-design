using OnlineShoppApp.Data;
using OnlineShoppApp.Models;
using System.Diagnostics;

namespace OnlineShoppApp.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        public CustomerService(ICustomerRepository repository) 
        {
            _repository = repository;
        }

        public async Task CreateCustomer(Customer customer)
        {
            try
            {
                if (customer == null)
                {
                    Console.WriteLine($"Customer object is null");
                }
                else
                {
                    await _repository.AddAsync( customer );
                    Console.WriteLine($"Customer recorded successfully!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public async Task<Customer> GetCustomerAsync(int Id)
        {
            return await _repository.GetCustomerAsync( Id );
        }

        public async Task ShowCustomerAsync(int Id)
        {
            try
            {
                var customer = await _repository.GetCustomerAsync(Id);
                if (customer == null)
                {
                    Debug.WriteLine($"Customer with id {Id} not found!");
                }
                else
                {
                    // Log customer details or any other relevant information
                    Console.WriteLine($"Customer:: [Id: {customer.Id}, Name: {customer.Name}, Email: {customer.Email}, Phone: {customer.Telephone}, Address: {customer.Address}]\n");
                    Console.WriteLine($"-------------");

                    foreach (var order in customer.Orders)
                    {
                        decimal _orderTotal = 0.0m;
                        Console.WriteLine($"Order:: [Id: {order.Id}, Order Date: {order.OrderDate}, Status: {order.Status}]\n");
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
                Debug.WriteLine(e, $"An error occurred while getting customer with id {Id}");
            }
        }

        public async Task ListCustomersAsync()
        {
            try
            {
                var customers = await _repository.GetCustomersAsync();
                if (customers == null)
                {
                    Debug.WriteLine($"No customer records found!");
                }
                else
                {
                    // Log customer details or any other relevant information
                   foreach (var customer in customers)
                    {
                        Console.WriteLine($"Customer:: [Id: {customer.Id}, Name: {customer.Name}, Email: {customer.Email}, Phone: {customer.Telephone}, Address: {customer.Address}]\n");
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e, $"An error occurred while fetching list of customers");
            }
        }
    }
}
