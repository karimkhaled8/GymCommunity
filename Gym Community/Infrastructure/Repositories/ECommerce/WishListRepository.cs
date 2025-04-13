using Gym_Community.Domain.Data.Models.E_comm;
using Gym_Community.Infrastructure.Context;
using Gym_Community.Infrastructure.Interfaces.ECommerce;
using Microsoft.EntityFrameworkCore;

namespace Gym_Community.Infrastructure.Repositories.ECommerce
{
    public class WishListRepository : IWishListRepository
    {
        private readonly ApplicationDbContext _context;
        public WishListRepository(ApplicationDbContext context)
        {
            _context = context; 
        }

        public async Task<Wishlist?> AddAsync(Wishlist wishlist)
        {
            _context.WhishLists.Add(wishlist);
            if (await _context.SaveChangesAsync() > 0) return wishlist;
            return null; 
        }

        public async Task<IEnumerable<Wishlist>> ListAsync()
        {
            return await _context.WhishLists.Include(w => w.Product).ToListAsync(); 
        }

        public async Task<Wishlist?> GetById(int id)
        {
            return await _context.WhishLists
                .Include(w => w.Product)
                .FirstOrDefaultAsync(w => w.Id == id); 
        }
        public async Task<Wishlist?> UpdateAsync(Wishlist wishlist)
        {
            _context.WhishLists.Update(wishlist);
            if (await _context.SaveChangesAsync() > 0) return wishlist;
            return null;
        }
        public async Task<bool> RemoveAsync(Wishlist wishlist)
        {
            _context.WhishLists.Remove(wishlist);
            if (await _context.SaveChangesAsync() > 0) return true;
            return false; 
        }
    }
}
