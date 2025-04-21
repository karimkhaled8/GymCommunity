using Gym_Community.Domain.Data.Models.Meals_and_Exercise;
using Gym_Community.Domain.Models;
using Gym_Community.Domain.Models.Coach_Plans;

namespace Gym_Community.Infrastructure.Interfaces.Training_Plans
{
    public interface ITrainingPlanRepository
    {
        Task<TrainingPlan?> GetByIdAsync(int id, string userId);
        Task<IEnumerable<TrainingPlan>> GetAllAsync(string userId);
        Task<IEnumerable<TrainingPlan>> GetByCoachIdAsync(string coachId);
        Task<bool> IsCoachAuthorizedAsync(int id, string coachId);
        Task AddAsync(TrainingPlan trainingPlan, string coachId);
        Task UpdateAsync(TrainingPlan trainingPlan, string coachId);
        Task DeleteAsync(TrainingPlan trainingPlan, string coachId);

        Task<AppUser> GetClientById(string userId);
    }
}
