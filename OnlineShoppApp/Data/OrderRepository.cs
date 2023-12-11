using Microsoft.EntityFrameworkCore;
using OnlineShoppApp.Models;
using System.Diagnostics;

namespace OnlineShoppApp.Data
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ShoppingContext _context;
        public OrderRepository(ShoppingContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            var existingOrder = await _context.Orders.FindAsync(order.Id);
            if (existingOrder == null)
            {
                Debug.WriteLine($"Order with id {order.Id} not found!");
            }
            else
            {
                // Update the existing order with the new values
                _context.Entry(existingOrder).CurrentValues.SetValues(order);

                // Mark the entity as modified (you might not need this if all properties are updated)
                _context.Entry(existingOrder).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
        }

        public async Task<Order> GetOrderAsync(int Id)
        {
            return await _context.Orders
                        .Include(o => o.Customer)
                        .Include(o => o.OrderLines)
                            .ThenInclude(o=>o.Product)
                    .FirstOrDefaultAsync(o => o.Id == Id);
        }
        public async Task<Order> GetOrderByCustomerAndDateAsync(int customerId, DateTime orderDate)
        {
            return await _context.Orders
                .FirstOrDefaultAsync(o => o.CustomerId == customerId && o.OrderDate == orderDate);
        }
        public async Task<ICollection<Order>> GetOrdersAsync()
        {
            return await _context.Orders
                        .Include(o => o.Customer)
                        .Include(o => o.OrderLines)
                            .ThenInclude(o=>o.Product)
                    .ToListAsync();
        }
    }
}
