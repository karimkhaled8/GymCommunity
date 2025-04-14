using Gym_Community.API.DTOs.E_comm;
using Gym_Community.Application.Interfaces.IE_comm;
using Gym_Community.Domain.Data.Models.E_comm;
using Gym_Community.Infrastructure.Interfaces.ECommerce;

namespace Gym_Community.Application.Services.E_comm
{
    public class ShoppingCartItemService : IShoppingCartItemService
    {
        private readonly IShoppingCartItemRepository _shoppingCartItemRepository;

        public ShoppingCartItemService(IShoppingCartItemRepository shoppingCartItemRepository)
        {
            _shoppingCartItemRepository = shoppingCartItemRepository;
        }

        public async Task<IEnumerable<ShoppingCartItemDTO>> GetShoppingCartItems()
        {
            var items = await _shoppingCartItemRepository.ListAsync();
            var itemDtos = items.Select(i => new ShoppingCartItemDTO
            {
                Id = i.Id,
                Quantity = i.Quantity,
                ShoppingCartID = i.ShoppingCartID,
                ProductID = i.ProductID,
                ProductName = i.Product?.Name,
                ProductPrice = i.Product?.Price ?? 0
            });

            return itemDtos;
        }

        public async Task<ShoppingCartItemDTO?> GetShoppingCartItemById(int id)
        {
            var item = await _shoppingCartItemRepository.GetById(id);
            if (item == null) return null;

            var itemDto = new ShoppingCartItemDTO
            {
                Id = item.Id,
                Quantity = item.Quantity,
                ShoppingCartID = item.ShoppingCartID,
                ProductID = item.ProductID,
                ProductName = item.Product?.Name,
                ProductPrice = item.Product?.Price ?? 0
            };

            return itemDto;
        }

        public async Task<int> CreateShoppingCartItem(ShoppingCartItemDTO shoppingCartItemDto)
        {
            var shoppingCartItem = new ShoppingCartItem
            {
                Quantity = shoppingCartItemDto.Quantity,
                ShoppingCartID = shoppingCartItemDto.ShoppingCartID,
                ProductID = shoppingCartItemDto.ProductID
            };

            var addedItem = await _shoppingCartItemRepository.AddAsync(shoppingCartItem);
            return addedItem?.Id ?? 0;
        }

        public async Task<bool> UpdateShoppingCartItem(int id, ShoppingCartItemDTO shoppingCartItemDto)
        {
            var existingItem = await _shoppingCartItemRepository.GetById(id);
            if (existingItem == null) return false;

            existingItem.Quantity = shoppingCartItemDto.Quantity;
            existingItem.ShoppingCartID = shoppingCartItemDto.ShoppingCartID;
            existingItem.ProductID = shoppingCartItemDto.ProductID;

            await _shoppingCartItemRepository.UpdateAsync(existingItem);
            return true;
        }

        public async Task<bool> DeleteShoppingCartItem(int id)
        {
            var item = await _shoppingCartItemRepository.GetById(id);
            if (item == null) return false;

            await _shoppingCartItemRepository.RemoveAsync(item);
            return true;
        }
    }
}
