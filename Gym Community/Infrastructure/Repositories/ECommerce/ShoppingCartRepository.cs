using Gym_Community.Domain.Data.Models.E_comm;
using Gym_Community.Infrastructure.Context;
using Gym_Community.Infrastructure.Interfaces.ECommerce;
using Microsoft.EntityFrameworkCore;

namespace Gym_Community.Infrastructure.Repositories.ECommerce
{
    public class ShoppingCartRepository :IShoppingCartRepository
    {
        private readonly ApplicationDbContext _context;
        public ShoppingCartRepository(ApplicationDbContext context)
        {
            _context = context; 
        }

        public async Task<ShoppingCart?> AddAsync(ShoppingCart shoppingCart)
        {
            _context.ShoppingCarts.Add(shoppingCart);
            if (await _context.SaveChangesAsync() > 0) 
                return shoppingCart; 

            return null;
        } 
        public async Task<IEnumerable<ShoppingCart>> ListAsync()
        {
            return await _context.ShoppingCarts
                .Include(s => s.UserId)
                .Include(s => s.ShoppingCartItems)
                .ToListAsync(); 
        }

        public async Task<ShoppingCart?> GetById(int id)
        {
            return await _context.ShoppingCarts
                .Include(s=>s.UserId)
                .Include(s=>s.ShoppingCartItems)
                .FirstOrDefaultAsync(s => s.Id == id); 
        }

        public async Task<ShoppingCart?> UpdateAsync(ShoppingCart shoppingCart)
        {
            _context.ShoppingCarts.Update(shoppingCart);
            if (await _context.SaveChangesAsync() > 0) return shoppingCart;
            return null; 
        }
        public async Task<bool> RemoveAsync(ShoppingCart shoppingCart)
        {
            _context.ShoppingCarts.Remove(shoppingCart);
            if (await _context.SaveChangesAsync() > 0) return true;
            return false; 
        }
    }
}
