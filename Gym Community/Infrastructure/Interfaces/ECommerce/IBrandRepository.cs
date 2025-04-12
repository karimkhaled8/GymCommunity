using Gym_Community.Domain.Data.Models.E_comm;

namespace Gym_Community.Infrastructure.Interfaces.ECommerce
{
    public interface IBrandRepository
    {
        public Task<Brand?> AddAsync(Brand brand);
        public Task<IEnumerable<Brand>> ListAsync();
        public Task<Brand?> GetById(int id);
        public Task<Brand?> UpdateAsync(Brand brand);
        public Task<bool> RemoveAsync(Brand brand); 
    }
}
