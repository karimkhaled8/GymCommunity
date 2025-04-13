using Gym_Community.Domain.Data.Models.Meals_and_Exercise;

namespace Gym_Community.Infrastructure.Interfaces.Meals_and_Exercise
{
    public interface IMealRepository
    {
        Task<IEnumerable<Meal>> GetAllAsync();
        Task<Meal?> GetByIdAsync(int id);
        Task AddAsync(Meal meal);
        Task UpdateAsync(Meal meal);
        Task DeleteAsync(int id);
        Task<IEnumerable<Meal>> GetByNameAsync(string name, bool? isSupplement = null);
        Task<IEnumerable<Meal>> GetBySupplementStatusAsync(bool isSupplement);
    }
}
