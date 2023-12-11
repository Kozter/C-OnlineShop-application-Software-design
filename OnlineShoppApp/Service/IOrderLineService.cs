using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppApp.Service
{
    public interface IOrderLineService
    {
        public Task CreateAsync(OrderLine orderLine);
        public Task<OrderLine> GetOrderLineAsync(int Id);
    }
}
