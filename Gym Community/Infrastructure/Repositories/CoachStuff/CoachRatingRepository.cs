using Gym_Community.Domain.Models.CoachStuff;
using Gym_Community.Infrastructure.Context;
using Gym_Community.Infrastructure.Interfaces.CoachStuff;
using Microsoft.EntityFrameworkCore;

namespace Gym_Community.Infrastructure.Repositories.CoachStuff
{
    public class CoachRatingRepository :ICoachRatingRepository
    {
        private readonly ApplicationDbContext _context;

        public CoachRatingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CoachRating>> GetByCoachIdAsync(string coachId)
        {
            return await _context.CoachRatings
                .Where(r => r.CoachId == coachId)
                .ToListAsync();
        }

        public async Task<CoachRating?> GetByIdAsync(int id)
        {
            return await _context.CoachRatings.FindAsync(id);
        }

        public async Task AddAsync(CoachRating rating)
        {
            await _context.CoachRatings.AddAsync(rating);
        }

        public void Delete(CoachRating rating)
        {
            _context.CoachRatings.Remove(rating);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
