using Gym_Community.Domain.Models.Gym;
using Gym_Community.Infrastructure.Context;
using Gym_Community.Infrastructure.Interfaces.Gym;
using Microsoft.EntityFrameworkCore;

namespace Gym_Community.Infrastructure.Repositories.Gym
{
    public class GymPlanRepository:IGymPlanRepository
    {
        private readonly ApplicationDbContext _context;

        public GymPlanRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GymPlan?> AddAsync(GymPlan plan)
        {
            await _context.GymPlans.AddAsync(plan);
            return await _context.SaveChangesAsync() > 0 ? plan : null;
        }

        public async Task<IEnumerable<GymPlan>> ListAsync()
        {
            return await _context.GymPlans.Include(g => g.UserSubscriptions).Include(g=>g.Gym).ToListAsync();
        }

        public async Task<GymPlan?> GetByIdAsync(int id)
        {
            return await _context.GymPlans.Include(g => g.UserSubscriptions).Include(g => g.Gym)
                                          .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<GymPlan>> GetByGymIdAsync(int gymId)
        {
            return await _context.GymPlans.Where(p => p.GymId == gymId)
                                          .Include(g => g.UserSubscriptions).Include(g => g.Gym)
                                          .ToListAsync();
        }

        public async Task<GymPlan?> UpdateAsync(GymPlan plan)
        {
            _context.GymPlans.Update(plan);
            return await _context.SaveChangesAsync() > 0 ? plan : null;
        }

        public async Task<bool> DeleteAsync(GymPlan plan)
        {
            _context.GymPlans.Remove(plan);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
