using Gym_Community.Domain.Models.Gym;
using Gym_Community.Infrastructure.Context;
using Gym_Community.Infrastructure.Interfaces.Gym;
using Microsoft.EntityFrameworkCore;

namespace Gym_Community.Infrastructure.Repositories.Gym
{
    public class GymCoachRepository:IGymCoachRepository
    {
        private readonly ApplicationDbContext _context;

        public GymCoachRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GymCoach?> AddAsync(GymCoach gymCoach)
        {
            await _context.GymCoaches.AddAsync(gymCoach);
            return await _context.SaveChangesAsync() > 0 ? gymCoach : null;
        }

        public async Task<IEnumerable<GymCoach>> ListAsync()
        {
            return await _context.GymCoaches
                .Include(gc => gc.Coach)
                .Include(gc => gc.Gym)
                .ToListAsync();
        }

        public async Task<GymCoach?> GetByIdAsync(int id)
        {
            return await _context.GymCoaches
                .Include(gc => gc.Coach)
                .Include(gc => gc.Gym)
                .FirstOrDefaultAsync(gc => gc.Id == id);
        }

        public async Task<GymCoach?> UpdateAsync(GymCoach gymCoach)
        {
            _context.GymCoaches.Update(gymCoach);
            return await _context.SaveChangesAsync() > 0 ? gymCoach : null;
        }

        public async Task<bool> DeleteAsync(GymCoach gymCoach)
        {
            _context.GymCoaches.Remove(gymCoach);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<GymCoach>> GetByGymId(int gymId)
        {
            return await _context.GymCoaches
            .Include(gc => gc.Coach)
            .Where(gc => gc.GymId == gymId)
            .ToListAsync();
        }
    }
}
