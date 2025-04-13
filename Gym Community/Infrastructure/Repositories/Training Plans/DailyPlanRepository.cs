using Gym_Community.Domain.Data.Models.Meals_and_Exercise;
using Gym_Community.Infrastructure.Context;
using Gym_Community.Infrastructure.Repositories.Training_Plans;
using Microsoft.EntityFrameworkCore;

namespace Gym_Community.Infrastructure.Interfaces.Training_Plans
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

        public async Task<DailyPlan> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(dp => dp.WeekPlan)
                .FirstOrDefaultAsync(dp => dp.Id == id);
        }

        public async Task<IEnumerable<DailyPlan>> GetAllAsync()
        {
            return await _dbSet
                .Include(dp => dp.WeekPlan)
                .ToListAsync();
        }

        public async Task AddAsync(DailyPlan dailyPlan)
        {
            await _dbSet.AddAsync(dailyPlan);
            await _context.SaveChangesAsync(); // ✅ auto save
        }

        public async Task UpdateAsync(DailyPlan dailyPlan)
        {
            _dbSet.Update(dailyPlan);
            await _context.SaveChangesAsync(); // ✅ auto save
        }

        public async Task DeleteAsync(DailyPlan dailyPlan)
        {
            _dbSet.Remove(dailyPlan);
            await _context.SaveChangesAsync(); // ✅ auto save
        }
    }
}
