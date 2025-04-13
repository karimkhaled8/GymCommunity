using Gym_Community.Domain.Data.Models.E_comm;

namespace Gym_Community.Infrastructure.Interfaces.ECommerce
{
    public interface IOrderItemRepository
    {
        public Task<OrderItem?> AddAsync(OrderItem orderItem);
        public Task<IEnumerable<OrderItem>> ListAsync();
        public Task<OrderItem?> GetById(int id);
        public Task<OrderItem?> UpdateAsync(OrderItem orderItem);
        public Task<bool> RemoveAsync(OrderItem orderItem);
    }
}
