using Microsoft.EntityFrameworkCore;

namespace OnlineShoppApp.Data
{
    public class PaymentRepository
    {
        private readonly ShoppingContext _context;
        public PaymentRepository(ShoppingContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
        }

        public async Task<Payment> GetPaymentAsync(int Id)
        {
            return await _context.Payments
                        .Include(p => p.Customer)
                        .Include(p => p.Order)
                    .FirstOrDefaultAsync(p => p.Id == Id);
        }

        public async Task<ICollection<Payment>> GetPaymentsAsync()
        {
            return await _context.Payments
                        .Include(p => p.Customer)
                        .Include(p => p.Order)
                       .ToListAsync();
        }
    }
}
