using Gym_Community.API.DTOs.E_comm;

namespace Gym_Community.Application.Interfaces.IE_comm
{
    public interface IOrderService
    {
        Task<OrderDto?> CreateOrderAsync(OrderDto orderDto);
        Task<IEnumerable<OrderDto>> GetOrdersAsync();
        public Task<IEnumerable<OrderDto>> GetUserOrderAsync(string userId);
        Task<OrderDto?> GetOrderByIdAsync(int id);
        Task<OrderDto?> UpdateOrderAsync(int id, OrderDto orderDto);
        Task<bool> RemoveOrderAsync(int id);
    }
}
