using Gym_Community.Domain.Data.Models.Meals_and_Exercise;

namespace Gym_Community.Infrastructure.Interfaces.Training_Plans
{
    public interface ITrainingPlanRepository
    {
        public interface ITrainingPlanRepository
        {
            Task<TrainingPlan> GetByIdAsync(int id);
            Task<IEnumerable<TrainingPlan>> GetAllAsync();
            Task AddAsync(TrainingPlan trainingPlan);
            Task UpdateAsync(TrainingPlan trainingPlan);
            Task DeleteAsync(TrainingPlan trainingPlan);
        }
    }
    }
