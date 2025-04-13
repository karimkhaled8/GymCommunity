using Gym_Community.Domain.Data.Models.Meals_and_Exercise;

namespace Gym_Community.Infrastructure.Interfaces.Meals_and_Exercise
{
    public interface IExerciseRepository
    {
        Task<IEnumerable<Exercise>> GetAllAsync();
        Task<Exercise?> GetByIdAsync(int id);
        Task<IEnumerable<Exercise>> GetByMuscleGroupAsync(int muscleGroupId);
        Task AddAsync(Exercise exercise);
        Task UpdateAsync(Exercise exercise);
        Task DeleteAsync(int id);
    }
}
