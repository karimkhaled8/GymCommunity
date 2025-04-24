using Gym_Community.Domain.Data.Models.Payment_and_Shipping;
using Gym_Community.Infrastructure.Context;
using Gym_Community.Infrastructure.Interfaces.ECommerce;
using Microsoft.EntityFrameworkCore;

namespace Gym_Community.Infrastructure.Repositories.ECommerce
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;
        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Payment?> AddAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            if (await _context.SaveChangesAsync() > 0)
            {
                return payment;
            }
            return null;
        }

        public async Task<Payment?> GetById(int id)
        {
            return await _context.Payments
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Payment>> ListAsync()
        {
            return await _context.Payments
                .ToListAsync();
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var payment = await _context.Payments.FirstOrDefaultAsync(p => p.Id == id);
            if (payment == null)
                return false;

           _context.Payments.Remove(payment);
           return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Payment?> UpdateAsync(Payment payment)
        {
            _context.Payments.Update(payment);
            if (await _context.SaveChangesAsync() > 0)
            {
                return payment;
            }
            return null;
        }
    }
}
