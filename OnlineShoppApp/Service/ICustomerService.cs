using OnlineShoppApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppApp.Service
{
    public interface ICustomerService
    {
        public Task CreateCustomer(Customer customer);
        public Task<Customer> GetCustomerAsync(int Id);
        public Task ShowCustomerAsync(int Id);
        public Task ListCustomersAsync();
    }
}
