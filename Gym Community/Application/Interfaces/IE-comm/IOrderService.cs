using Gym_Community.API.DTOs;
using Gym_Community.API.DTOs.E_comm;
using Gym_Community.Domain.Data.Models.E_comm;
using Gym_Community.Domain.Enums;

namespace Gym_Community.Application.Interfaces.IE_comm
{
    public interface IOrderService
    {
        Task<OrderDto?> CreateOrderAsync(OrderDto orderDto,string userId);
        Task<PageResult<OrderDto>> GetOrdersAsync(string query, int page, int eleNo, string sort, ShippingStatus? status, DateOnly? date);
        public Task<IEnumerable<OrderDto>> GetUserOrderAsync(string userId);
        Task<OrderDto?> GetOrderByIdAsync(int id);
        Task<OrderDto?> UpdateOrderAsync(int id, OrderDto orderDto);
        Task<bool> RemoveOrderAsync(int id);
    }
}
