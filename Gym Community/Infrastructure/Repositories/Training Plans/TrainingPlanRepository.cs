using Gym_Community.Domain.Data.Models.Meals_and_Exercise;
using Gym_Community.Domain.Models;
using Gym_Community.Domain.Models.Coach_Plans;
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

        public async Task<TrainingPlan?> GetByIdAsync(int id, string userId)
        {
            return await _dbSet
                .Include(tp => tp.Coach)
                .Include(tp => tp.WeekPlans)
                .ThenInclude(wp => wp.WorkoutDays)
                .Where(tp => tp.Id == id && (tp.ClientId == userId || tp.CoachId == userId))
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TrainingPlan>> GetAllAsync(string userId)
        {
            return await _dbSet
                //.Include(tp => tp.WeekPlans)
                .Include(tp=>tp.Coach)
                .Include(tp => tp.Client)
                .Where(tp => tp.ClientId == userId || tp.CoachId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<TrainingPlan>> GetByCoachIdAsync(string coachId)
        {
            return await _dbSet
                .Include(tp => tp.WeekPlans)
                .Where(tp => tp.CoachId == coachId)
                .ToListAsync();
        }

        public async Task<bool> IsCoachAuthorizedAsync(int id, string coachId)
        {
            return await _dbSet
                .AnyAsync(tp => tp.Id == id && tp.CoachId == coachId);
        }

        public async Task AddAsync(TrainingPlan trainingPlan, string coachId)
        {
            if (trainingPlan.CoachId != coachId)
            {
                throw new UnauthorizedAccessException("You can only create training plans for yourself");
            }

            await _dbSet.AddAsync(trainingPlan);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TrainingPlan trainingPlan, string coachId)
        {
            if (trainingPlan.CoachId != coachId)
            {
                throw new UnauthorizedAccessException("You can only update your own training plans");
            }

            _dbSet.Update(trainingPlan);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TrainingPlan trainingPlan, string coachId)
        {
            if (trainingPlan.CoachId != coachId)
            {
                throw new UnauthorizedAccessException("You can only delete your own training plans");
            }

            _dbSet.Remove(trainingPlan);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable< AppUser>> GetAllCoachClients(string coachId)
        {
            return await _dbSet
            .Where(tp => tp.CoachId == coachId && tp.ClientId != null)
             .Select(tp => tp.Client)
             .Distinct()
             .ToListAsync();

        }
    }
}
