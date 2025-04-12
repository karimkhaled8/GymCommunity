using Gym_Community.Domain.Data.Models.E_comm;

namespace Gym_Community.Infrastructure.Interfaces.ECommerce
{
    public interface ICategoryRepository
    {
        public Task<Category?> AddAsync(Category category);
        public Task<IEnumerable<Category>> ListAsync();
        public Task<Category?> GetById(int id);
        public Task<Category?> UpdateAsync(Category category);
        public Task<bool> RemoveAsync(Category category);
    }
}
