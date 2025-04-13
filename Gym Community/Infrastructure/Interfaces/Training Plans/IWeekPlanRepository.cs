using Gym_Community.Domain.Models.Coach_Plans;

namespace Gym_Community.Infrastructure.Interfaces.Training_Plans
{
    public interface IWeekPlanRepository
    {
        Task<WeekPlan> GetByIdAsync(int id);
        Task<IEnumerable<WeekPlan>> GetAllAsync();
        Task AddAsync(WeekPlan weekPlan);
        Task UpdateAsync(WeekPlan weekPlan);
        Task DeleteAsync(WeekPlan weekPlan);
    }
}
