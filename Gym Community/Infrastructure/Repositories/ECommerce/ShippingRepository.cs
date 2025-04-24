using Gym_Community.Domain.Data.Models.Payment_and_Shipping;
using Gym_Community.Infrastructure.Context;
using Gym_Community.Infrastructure.Interfaces.ECommerce;
using Microsoft.EntityFrameworkCore;

namespace Gym_Community.Infrastructure.Repositories.ECommerce
{
    public class ShippingRepository : IShippingRepository
    {
        private readonly ApplicationDbContext _context;
        public ShippingRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Shipping?> AddAsync(Shipping shipping)
        {
            await _context.Shippings.AddAsync(shipping);
            if (await _context.SaveChangesAsync() > 0)
            {
                return shipping;
            }
            return null;
        }

        public async Task<Shipping?> GetById(int id)
        {
            return await _context.Shippings
                .Include(s => s.Order)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Shipping> GetByOrderId(int orderId)
        {
            return await _context.Shippings
                .Include(s => s.Order)
                .FirstOrDefaultAsync(s => s.OrderID == orderId);
        }

        public async Task<IEnumerable<Shipping>> ListAsync()
        {
            return await _context.Shippings
                .Include(s => s.Order)
                .ToListAsync();
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var shipping = _context.Shippings.FirstOrDefaultAsync(s=>s.Id==id);
            if (shipping != null)
            {
                _context.Shippings.Remove(shipping.Result);
                return await _context.SaveChangesAsync() > 0;
            }
            return false; 
        }

        public async Task<Shipping?> UpdateAsync(Shipping shipping)
        {
            _context.Shippings.Update(shipping);
            if (await _context.SaveChangesAsync() > 0)
            {
                return shipping;
            }
            return null;
        }
    }
}
