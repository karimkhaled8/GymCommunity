using Gym_Community.Domain.Data.Models.E_comm;
using Gym_Community.Infrastructure.Context;
using Gym_Community.Infrastructure.Interfaces.ECommerce;
using Microsoft.EntityFrameworkCore;

namespace Gym_Community.Infrastructure.Repositories.ECommerce
{
    public class ShoppingCartItemRepository : IShoppingCartItemRepository
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartItemRepository(ApplicationDbContext context)
        {
            _context = context; 
        }

        public async Task<ShoppingCartItem?> AddAsync(ShoppingCartItem shoppingCartItem)
        {
            _context.ShoppingCartItems.Add(shoppingCartItem);
            if (await _context.SaveChangesAsync() > 0) return shoppingCartItem;
            return null;
        }

        public async Task<IEnumerable<ShoppingCartItem>> ListAsync()
        {
            return await _context.ShoppingCartItems
                .Include(i => i.Product)
                .Include(i => i.ShoppingCart)
                .ToListAsync(); 
        }
        public async Task<ShoppingCartItem?> GetById(int id)
        {
            return await _context.ShoppingCartItems
                .Include(i => i.Product)
                .Include(i => i.ShoppingCart)
                .FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<ShoppingCartItem?> UpdateAsync(ShoppingCartItem shoppingCartItem)
        {
            _context.ShoppingCartItems.Update(shoppingCartItem);
            if (await _context.SaveChangesAsync() > 0) return shoppingCartItem;
            return null;
        }
        public async Task<bool> RemoveAsync(ShoppingCartItem shoppingCartItem)
        {
            _context.ShoppingCartItems.Remove(shoppingCartItem);
            if (await _context.SaveChangesAsync() > 0) return true;
            return false; 
        }
    }
}
