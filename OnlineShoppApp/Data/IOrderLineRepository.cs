using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppApp.Data
{
    public interface IOrderLineRepository
    {
        public Task AddAsync(OrderLine orderLine);
        public Task<OrderLine> GetOrderLineAsync(int Id);
        public Task<ICollection<OrderLine>> GetOrderLinesAsync();
    }
}
