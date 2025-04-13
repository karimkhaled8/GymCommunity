using Gym_Community.Domain.Data.Models.E_comm;
using Gym_Community.Infrastructure.Context;
using Gym_Community.Infrastructure.Interfaces.ECommerce;
using Microsoft.EntityFrameworkCore;

namespace Gym_Community.Infrastructure.Repositories.ECommerce
{
    public class OrderItemsRepository : IOrderItemRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderItemsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OrderItem?> AddAsync(OrderItem orderItem)
        {
            await _context.OrderItems.AddAsync(orderItem);
            if (await _context.SaveChangesAsync() > 0)
            {
                return orderItem;
            }
            else
            {
                return null;
            }
        }
        public async Task<IEnumerable<OrderItem>> ListAsync()
        {
            return await _context.OrderItems
                .Include(oi => oi.Order)
                .Include(oi => oi.Product)
                .ToListAsync();
        }
        public async Task<OrderItem?> GetById(int id)
        {
            return await _context.OrderItems
                                 .Include(oi => oi.Order)
                                 .Include(oi => oi.Product)
                                 .FirstOrDefaultAsync(oi => oi.Id == id);
        }
        public async Task<OrderItem?> UpdateAsync(OrderItem orderItem)
        {
            _context.OrderItems.Update(orderItem);

            if (await _context.SaveChangesAsync() > 0)
            {
                return orderItem;
            }
            else
            {
                return null;
            }

        }
        public async Task<bool> RemoveAsync(OrderItem orderItem)
        {
            if (orderItem == null)
            {
                return false;
            }
            _context.OrderItems.Remove(orderItem);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
