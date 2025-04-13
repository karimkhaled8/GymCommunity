using Gym_Community.Domain.Data.Models.E_comm;
using Gym_Community.Infrastructure.Context;
using Gym_Community.Infrastructure.Interfaces.ECommerce;
using Microsoft.EntityFrameworkCore;

namespace Gym_Community.Infrastructure.Repositories.ECommerce
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;
        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context; 
        }

        public async Task<Review?> AddAsync(Review review)
        {
            await _context.Reviews.AddAsync(review);
            if (await _context.SaveChangesAsync() > 0)
            {
                return review;
            }
            else
            {
                return null;
            }
        }
        public async Task<IEnumerable<Review>> ListAsync()
        {
            return await _context.Reviews
                .Include(r => r.Product)
                .Include(r => r.AppUser)
                .ToListAsync();
        }
        public async Task<Review?> GetById(int id)
        {
            return await _context.Reviews
                .Include(r => r.Product)
                .Include(r => r.AppUser)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
        public async Task<Review?> UpdateAsync(Review review)
        {
            _context.Reviews.Update(review);
            if (await _context.SaveChangesAsync() > 0)
            {
                return review;
            }
            else
            {
                return null;
            }
        }
        public async Task<bool> RemoveAsync(Review review)
        {
            if (review == null)
            {
                return false;
            }
            _context.Reviews.Remove(review);
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
