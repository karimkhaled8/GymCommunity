using Gym_Community.Domain.Models.Gym;

namespace Gym_Community.Infrastructure.Interfaces.Gym
{
    public interface IUserSubscriptionRepository
    {
        Task<IEnumerable<UserSubscription>> GetAllAsync();
        Task<UserSubscription?> GetByIdAsync(int id);
        Task<IEnumerable<UserSubscription>> GetByGymIdAsync(int gymId);
        Task<IEnumerable<UserSubscription>> GetByPlanIdAsync(int planId);
        Task<UserSubscription> AddAsync(UserSubscription subscription);
        Task<UserSubscription?> UpdateAsync(UserSubscription subscription);
        Task<bool> DeleteAsync(UserSubscription subscription);
    }
}
