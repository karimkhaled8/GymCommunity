using Gym_Community.API.DTOs;
using Gym_Community.Domain.Data.Models.E_comm;
using Gym_Community.Domain.Enums;

namespace Gym_Community.Infrastructure.Interfaces.ECommerce
{
    public interface IOrderRepository
    {
        public Task<Order?> AddAsync(Order order);
        public Task<PageResult<Order>> ListAsync(string query, int page, int eleNo, string sort, ShippingStatus? status, DateOnly? date);
        public Task<IEnumerable<Order>> ListUserOrdersAsync(string userId);
        public Task<Order?> GetById(int id);
        public Task<Order?> UpdateAsync(Order order);
        public Task<bool> RemoveAsync(Order order);
    }
}
