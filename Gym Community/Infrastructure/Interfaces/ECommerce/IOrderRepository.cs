using Gym_Community.Domain.Data.Models.E_comm;

namespace Gym_Community.Infrastructure.Interfaces.ECommerce
{
    public interface IOrderRepository
    {
        public Task<Order?> AddAsync(Order order);
        public Task<IEnumerable<Order>> ListAsync();
        public Task<Order?> GetById(int id);
        public Task<Order?> UpdateAsync(Order order);
        public Task<bool> RemoveAsync(Order order);
    }
}
