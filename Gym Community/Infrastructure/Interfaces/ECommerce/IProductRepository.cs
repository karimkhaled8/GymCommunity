using Gym_Community.Domain.Data.Models.E_comm;
using Gym_Community.Domain.Data.Models.E_comms;

namespace Gym_Community.Infrastructure.Interfaces.ECommerce
{
    public interface IProductRepository
    {
        public Task<Product?> AddAsync(Product product);
        public Task<IEnumerable<Product>> ListAsync();
        public Task<Product?> GetById(int id);
        public Task<Product?> UpdateAsync(Product product);
        public Task<bool> RemoveAsync(Product product);
    }
}
