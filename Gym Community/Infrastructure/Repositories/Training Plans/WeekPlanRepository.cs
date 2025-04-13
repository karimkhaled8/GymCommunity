using Gym_Community.Domain.Models.Coach_Plans;
using Gym_Community.Infrastructure.Context;
using Gym_Community.Infrastructure.Interfaces.Training_Plans;
using Microsoft.EntityFrameworkCore;

namespace Gym_Community.Infrastructure.Repositories.Training_Plans
{
    public class WeekPlanRepository : IWeekPlanRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<WeekPlan> _dbSet;

        public WeekPlanRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<WeekPlan>();
        }

        public async Task<WeekPlan> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(wp => wp.WorkoutDays)
                .FirstOrDefaultAsync(wp => wp.Id == id);
        }

        public async Task<IEnumerable<WeekPlan>> GetAllAsync()
        {
            return await _dbSet
                .Include(wp => wp.WorkoutDays)
                .ToListAsync();
        }

        public async Task AddAsync(WeekPlan weekPlan)
        {
            await _dbSet.AddAsync(weekPlan);
            await _context.SaveChangesAsync(); // save here ✅
        }

        public async Task UpdateAsync(WeekPlan weekPlan)
        {
            _dbSet.Update(weekPlan);
            await _context.SaveChangesAsync(); // save here ✅
        }

        public async Task DeleteAsync(WeekPlan weekPlan)
        {
            _dbSet.Remove(weekPlan);
            await _context.SaveChangesAsync(); // save here ✅
        }
    }
}
