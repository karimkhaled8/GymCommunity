using Gym_Community.API.DTOs.E_comm;

namespace Gym_Community.Application.Interfaces.IE_comm
{
    public interface IOrderItemService
    {
        Task<OrderItemDto?> AddAsync(OrderItemDto dto);
        Task<IEnumerable<OrderItemDto>> GetAllAsync();
        Task<OrderItemDto?> GetByIdAsync(int id);
        Task<OrderItemDto?> UpdateAsync(OrderItemDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
