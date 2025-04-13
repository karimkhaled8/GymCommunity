using Gym_Community.Domain.Models.Coach_Plans;

namespace Gym_Community.Infrastructure.Interfaces.Training_Plans
{
    public interface IWeekPlanRepository
    {
        Task<WeekPlan?> GetByIdAsync(int id, string userId);
        Task<IEnumerable<WeekPlan>> GetAllAsync(string userId);
        Task<IEnumerable<WeekPlan>> GetByTrainingPlanIdAsync(int trainingPlanId, string userId);
        Task<bool> IsUserAuthorizedAsync(int id, string userId);
        Task AddAsync(WeekPlan weekPlan);
        Task UpdateAsync(WeekPlan weekPlan);
        Task DeleteAsync(WeekPlan weekPlan);
    }
}
