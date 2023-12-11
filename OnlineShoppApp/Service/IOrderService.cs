using OnlineShoppApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppApp.Service
{
    public interface IOrderService
    {
        public Task CreateAsync(Order order);
        public Task<Order> GetOrderAsync(int Id);
        public Task ShowOrderAsync(int Id);
        public Task ShowOrdersAsync();
        public Task<Order> GetOrderByCustomerAndDateAsync(int customerId, DateTime orderDate);
    }
}
