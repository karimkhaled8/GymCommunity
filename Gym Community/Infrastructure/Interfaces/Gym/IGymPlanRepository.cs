using Gym_Community.Domain.Models.Gym;

namespace Gym_Community.Infrastructure.Interfaces.Gym
{
    public interface IGymPlanRepository
    {
        Task<GymPlan?> AddAsync(GymPlan plan);
        Task<IEnumerable<GymPlan>> ListAsync();
        Task<GymPlan?> GetByIdAsync(int id);
        Task<IEnumerable<GymPlan>> GetByGymIdAsync(int gymId);
        Task<GymPlan?> UpdateAsync(GymPlan plan);
        Task<bool> DeleteAsync(GymPlan plan);
    }
}
