using Gym_Community.Domain.Data.Models.Meals_and_Exercise;
using Gym_Community.Infrastructure.Context;
using Gym_Community.Infrastructure.Interfaces.Training_Plans;
using Microsoft.EntityFrameworkCore;

namespace Gym_Community.Infrastructure.Repositories.Training_Plans
{
    public class DailyPlanRepository : IDailyPlanRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<DailyPlan> _dbSet;

        public DailyPlanRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<DailyPlan>();
        }

        public async Task<DailyPlan?> GetByIdAsync(int id, string userId)
        {
            return await _dbSet
                .Include(dp => dp.WeekPlan)
                    .ThenInclude(wp => wp.TrainingPlan)
                .Where(dp => dp.WeekPlan.TrainingPlan.ClientId == userId)
                .FirstOrDefaultAsync(dp => dp.Id == id);
        }

        public async Task<IEnumerable<DailyPlan>> GetAllAsync(string userId)
        {
            return await _dbSet
                .Include(dp => dp.WeekPlan)
                    .ThenInclude(wp => wp.TrainingPlan)
                .Where(dp => dp.WeekPlan.TrainingPlan.ClientId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<DailyPlan>> GetByWeekIdAsync(int weekPlanId, string userId)
        {
            return await _dbSet
                .Where(dp => dp.WeekPlanId == weekPlanId && dp.WeekPlan.TrainingPlan.ClientId == userId)
                .Include(dp => dp.WeekPlan)
                    .ThenInclude(wp => wp.TrainingPlan)
                .ToListAsync();
        }

        public async Task<bool> IsUserAuthorizedAsync(int id, string userId)
        {
            return await _dbSet
                .AnyAsync(dp => dp.Id == id && dp.WeekPlan.TrainingPlan.ClientId == userId);
        }

        public async Task AddAsync(DailyPlan dailyPlan)
        {
            await _dbSet.AddAsync(dailyPlan);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DailyPlan dailyPlan)
        {
            _dbSet.Update(dailyPlan);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(DailyPlan dailyPlan)
        {
            _dbSet.Remove(dailyPlan);
            await _context.SaveChangesAsync();
        }
    }
}
