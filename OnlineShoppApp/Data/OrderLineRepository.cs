using Microsoft.EntityFrameworkCore;

namespace OnlineShoppApp.Data
{
    public class OrderLineRepository: IOrderLineRepository
    {
        private readonly ShoppingContext _context;
        public OrderLineRepository(ShoppingContext context)
        {
            _context = context;
        }

        public async Task AddAsync(OrderLine orderLine)
        {
            await _context.OrderLines.AddAsync(orderLine);
            await _context.SaveChangesAsync();
        }

        public async Task<OrderLine>GetOrderLineAsync(int Id)
        {
            return await _context.OrderLines
                        .Include(o=>o.Product)
                        .Include(o=>o.Order)
                    .FirstOrDefaultAsync(o=>o.Id == Id);
        }

        public async Task<ICollection<OrderLine>> GetOrderLinesAsync()
        {
            return await _context.OrderLines
                        .Include(o => o.Product)
                        .Include(o => o.Order)
                    .ToListAsync();
        }
    }
}
