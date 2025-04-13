using Gym_Community.Domain.Data.Models.Meals_and_Exercise;

namespace Gym_Community.Infrastructure.Repositories.Training_Plans
{
    public interface IDailyPlanRepository
    {
        Task<DailyPlan> GetByIdAsync(int id);
        Task<IEnumerable<DailyPlan>> GetAllAsync();
        Task AddAsync(DailyPlan dailyPlan);
        Task UpdateAsync(DailyPlan dailyPlan);
        Task DeleteAsync(DailyPlan dailyPlan);
    }
}
