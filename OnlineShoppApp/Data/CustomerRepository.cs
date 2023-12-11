using Microsoft.EntityFrameworkCore;
using OnlineShoppApp.Models;
using System.Diagnostics;

namespace OnlineShoppApp.Data
{
   
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ShoppingContext _context;
        public CustomerRepository()
        {
            
        }
        public CustomerRepository(ShoppingContext context) 
        {
            _context = context;
        }

        public async Task AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<Customer> GetCustomerAsync(int id)
        {
            return await _context.Customers
                        .Include(c => c.Orders)
                            .ThenInclude(o=>o.OrderLines)
                    .FirstOrDefaultAsync(c =>c.Id == id);
        }
        public async Task<ICollection<Customer>> GetCustomersAsync()
        {
            return await _context.Customers
                        .Include(c=>c.Orders)
                            .ThenInclude(o=>o.OrderLines)
                    .ToListAsync();
        }

    }
}
