using Gym_Community.Domain.Models.Gym;
using Gym_Community.Infrastructure.Context;
using Gym_Community.Infrastructure.Interfaces.Gym;
using Microsoft.EntityFrameworkCore;

namespace Gym_Community.Infrastructure.Repositories.Gym
{
    public class UserSubscriptionRepository:IUserSubscriptionRepository
    {
        private readonly ApplicationDbContext _context;

        public UserSubscriptionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserSubscription> AddAsync(UserSubscription subscription)
        {
            await _context.UserSubscriptions.AddAsync(subscription);
            await _context.SaveChangesAsync();
            return subscription;
        }

        public async Task<bool> DeleteAsync(UserSubscription subscription)
        {
            _context.UserSubscriptions.Remove(subscription);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<UserSubscription>> GetAllAsync()
        {
            return await _context.UserSubscriptions
                .Include(u => u.User)
                .Include(u => u.Gym)
                .Include(u => u.Plan)
                .ToListAsync();
        }

        public async Task<UserSubscription?> GetByIdAsync(int id)
        { 
            return await _context.UserSubscriptions.Where(u => u.Id == id)
                .Include(u => u.User)
                .Include(u => u.Gym)
                .Include(u => u.Plan)
                .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<UserSubscription>> GetByUserIdAsync(string userId)
        {
            return await _context.UserSubscriptions.Where(x => x.UserId == userId)
                .Include(u => u.User)
                .Include(u => u.Gym)
                .Include(u => u.Plan)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserSubscription>> GetByGymIdAsync(int gymId)
        {
            return await _context.UserSubscriptions.Where(x => x.GymId == gymId)
                .Include(u => u.User)
                .Include(u => u.Gym)
                .Include(u => u.Plan)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserSubscription>> GetByPlanIdAsync(int planId)
        {
            return await _context.UserSubscriptions.Where(x => x.PlanId == planId)
                .Include(u => u.User)
                .Include(u => u.Gym)
                .Include(u => u.Plan)
                .ToListAsync();
        }

        public async Task<UserSubscription?> UpdateAsync(UserSubscription subscription)
        {
            _context.UserSubscriptions.Update(subscription);
            await _context.SaveChangesAsync();
            return subscription;
        }
    }
}
