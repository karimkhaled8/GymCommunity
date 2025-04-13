using Gym_Community.Domain.Data.Models.E_comm;
using Gym_Community.Infrastructure.Context;
using Gym_Community.Infrastructure.Interfaces.ECommerce;
using Microsoft.EntityFrameworkCore;

namespace Gym_Community.Infrastructure.Repositories.ECommerce
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly ApplicationDbContext _context;

        public WishlistRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Wishlist?> AddAsync(Wishlist wishlist)
        {
            await _context.WhishLists.AddAsync(wishlist);
            return (await _context.SaveChangesAsync()) > 0 ? wishlist : null;
        }

        public async Task<IEnumerable<Wishlist>> ListAsync()
        {
            return await _context.WhishLists
                .Include(w => w.AppUser)
                .Include(w => w.Product)
                .ToListAsync();
        }

        public async Task<Wishlist?> GetByIdAsync(int id)
        {
            return await _context.WhishLists
                .Include(w => w.AppUser)
                .Include(w => w.Product)
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<IEnumerable<Wishlist>> GetByUserIdAsync(string userId)
        {
            return await _context.WhishLists
                .Include(w => w.AppUser)
                .Include(w => w.Product)
                .Where(w => w.UserID == userId)
                .ToListAsync();
        }

        public async Task<Wishlist?> UpdateAsync(Wishlist wishlist)
        {
            _context.WhishLists.Update(wishlist);
            return (await _context.SaveChangesAsync()) > 0 ? wishlist : null;
        }

        public async Task<bool> RemoveAsync(Wishlist wishlist)
        {
            _context.WhishLists.Remove(wishlist);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> ProductExistsInWishlistAsync(string userId, int productId)
        {
            return await _context.WhishLists
                .AnyAsync(w => w.UserID == userId && w.ProductID == productId);
        }
    }
}
