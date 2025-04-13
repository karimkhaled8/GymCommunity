using Gym_Community.Domain.Data.Models.Meals_and_Exercise;

namespace Gym_Community.Infrastructure.Interfaces.Meals_and_Exercise
{
        public interface IMuscleGroupRepository
        {
            Task<IEnumerable<MuscleGroup>> GetAllAsync();
            Task<MuscleGroup?> GetByIdAsync(int id);
            Task AddAsync(MuscleGroup muscleGroup);
            Task UpdateAsync(MuscleGroup muscleGroup);
            Task DeleteAsync(int id);
        }
    
}
