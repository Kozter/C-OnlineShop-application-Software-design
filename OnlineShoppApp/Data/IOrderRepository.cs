using OnlineShoppApp.Models;

namespace OnlineShoppApp.Data
{
    public interface IOrderRepository
    {
        public Task AddAsync(Order order);
        public Task UpdateAsync(Order order);
        public Task<Order> GetOrderAsync(int Id);
        public Task<ICollection<Order>> GetOrdersAsync();
        public Task<Order> GetOrderByCustomerAndDateAsync(int customerId, DateTime orderDate);
    }
}
