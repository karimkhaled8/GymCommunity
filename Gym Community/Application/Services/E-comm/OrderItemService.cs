using Gym_Community.API.DTOs.E_comm;
using Gym_Community.Application.Interfaces.IE_comm;
using Gym_Community.Domain.Data.Models.E_comm;
using Gym_Community.Infrastructure.Interfaces.ECommerce;

namespace Gym_Community.Application.Services.E_comm
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _repository;

        public OrderItemService(IOrderItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<OrderItemDto?> AddAsync(OrderItemDto dto)
        {
            var entity = new OrderItem
            {
                OrderID = dto.OrderId,
                ProductID = dto.ProductId,
                Quantity = dto.Quantity,
                Price = dto.Price
            };

            var result = await _repository.AddAsync(entity);

            return result == null ? null : MapToDto(result);
        }

        public async Task<IEnumerable<OrderItemDto>> GetAllAsync()
        {
            var items = await _repository.ListAsync();
            return items.Select(MapToDto);
        }

        public async Task<OrderItemDto?> GetByIdAsync(int id)
        {
            var item = await _repository.GetById(id);
            return item == null ? null : MapToDto(item);
        }

        public async Task<OrderItemDto?> UpdateAsync(OrderItemDto dto)
        {
            var existing = await _repository.GetById(dto.Id);
            if (existing == null) return null;

            existing.OrderID = dto.OrderId;
            existing.ProductID = dto.ProductId;
            existing.Quantity = dto.Quantity;
            existing.Price = dto.Price;

            var updated = await _repository.UpdateAsync(existing);
            return updated == null ? null : MapToDto(updated);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _repository.GetById(id);
            if (item == null) return false;

            return await _repository.RemoveAsync(item);
        }

        private OrderItemDto MapToDto(OrderItem item)
        {
            return new OrderItemDto
            {
                Id = item.Id,
                OrderId = item.OrderID,
                ProductId = item.ProductID,
                Quantity = item.Quantity,
                Price = item.Price,
                ProductName = item.Product?.Name ?? string.Empty
            };
        }
    }
}