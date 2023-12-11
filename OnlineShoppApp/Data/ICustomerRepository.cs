using OnlineShoppApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppApp.Data
{
    public interface ICustomerRepository
    {
        public Task AddAsync(Customer customer);
        public Task<Customer> GetCustomerAsync(int id);
        public Task<ICollection<Customer>> GetCustomersAsync();
    }
}
