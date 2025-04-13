using Gym_Community.Domain.Data.Models.E_comm;
using Gym_Community.Infrastructure.Context;
using Gym_Community.Infrastructure.Interfaces.ECommerce;
using Microsoft.EntityFrameworkCore;

namespace Gym_Community.Infrastructure.Repositories.ECommerce
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context)
        {
           _context = context;
        }

        public async Task<Order?> AddAsync(Order order)
        {
            _context.Orders.Add(order);
            if (await _context.SaveChangesAsync() > 0)
            {
                return order;
            }
            else {
                return null; 
            }
        }
    
        public async Task<IEnumerable<Order>> ListAsync()
        {
            return await _context.Orders
                .Include(o => o.AppUser)
                .Include(o => o.Payment)
                .Include(o => o.Shipping)
                .ToListAsync();
        }

        public async Task<Order?> GetById(int id)
        {
            return await _context.Orders
                .Include(o => o.AppUser)
                .Include(o => o.Payment)
                .Include(o => o.Shipping)
                .FirstOrDefaultAsync(o => o.OrderID == id);
        }
        public async Task<Order?> UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            if (await _context.SaveChangesAsync() > 0)
            {
                return order;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> RemoveAsync(Order order)
        {

            if (order == null) return false;

            _context.Orders.Remove(order);

            return await _context.SaveChangesAsync() > 0;
        }

    }
}
