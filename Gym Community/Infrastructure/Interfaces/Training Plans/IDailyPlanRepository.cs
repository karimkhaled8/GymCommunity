using Gym_Community.Domain.Data.Models.Meals_and_Exercise;

namespace Gym_Community.Infrastructure.Interfaces.Training_Plans
{
    public interface IDailyPlanRepository
    {
        Task<DailyPlan?> GetByIdAsync(int id, string userId);
        Task<IEnumerable<DailyPlan>> GetAllAsync(string userId);
        Task<IEnumerable<DailyPlan>> GetByWeekIdAsync(int weekPlanId, string userId);
        Task<bool> IsUserAuthorizedAsync(int id, string userId);
        Task AddAsync(DailyPlan dailyPlan);
        Task UpdateAsync(DailyPlan dailyPlan);
        Task DeleteAsync(DailyPlan dailyPlan);

        
    }
}
