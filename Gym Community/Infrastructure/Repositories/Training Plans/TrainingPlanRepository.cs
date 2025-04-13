using Gym_Community.Domain.Data.Models.Meals_and_Exercise;
using Gym_Community.Infrastructure.Context;
using Gym_Community.Infrastructure.Interfaces.Training_Plans;
using Microsoft.EntityFrameworkCore;

namespace Gym_Community.Infrastructure.Repositories.Training_Plans
{
    public class TrainingPlanRepository : ITrainingPlanRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<TrainingPlan> _dbSet;

        public TrainingPlanRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TrainingPlan>();
        }

        public async Task<TrainingPlan> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(tp => tp.WeekPlans)
                .FirstOrDefaultAsync(tp => tp.Id == id);
        }

        public async Task<IEnumerable<TrainingPlan>> GetAllAsync()
        {
            return await _dbSet
                .Include(tp => tp.WeekPlans)
                .ToListAsync();
        }

        public async Task AddAsync(TrainingPlan trainingPlan)
        {
            await _dbSet.AddAsync(trainingPlan);
            await _context.SaveChangesAsync();  // save here ✅
        }

        public async Task UpdateAsync(TrainingPlan trainingPlan)
        {
            _dbSet.Update(trainingPlan);
            await _context.SaveChangesAsync();  // save here ✅
        }

        public async Task DeleteAsync(TrainingPlan trainingPlan)
        {
            _dbSet.Remove(trainingPlan);
            await _context.SaveChangesAsync();  // save here ✅
        }
    }
}
