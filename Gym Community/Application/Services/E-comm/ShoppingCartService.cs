using Gym_Community.API.DTOs.E_comm;
using Gym_Community.Application.Interfaces.IE_comm;
using Gym_Community.Domain.Data.Models.E_comm;
using Gym_Community.Infrastructure.Interfaces.ECommerce;

namespace Gym_Community.Application.Services.E_comm
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<IEnumerable<ShoppingCartDTO>> GetShoppingCartsAsync()
        {
            var carts = await _shoppingCartRepository.ListAsync();
            return carts.Select(c => new ShoppingCartDTO
            {
                Id = c.Id,
                UserId = c.UserId,
                Items = c.ShoppingCartItems.Select(i => new ShoppingCartItemDTO
                {
                    Id = i.Id,
                    ProductID = i.ProductID,
                    Quantity = i.Quantity,
                    ProductName = i.Product?.Name,
                    ProductPrice = i.Product?.Price ?? 0
                }).ToList()
            });
        }

        public async Task<ShoppingCartDTO?> GetShoppingCartByIdAsync(int id)
        {
            var cart = await _shoppingCartRepository.GetById(id);
            if (cart == null) return null;

            return new ShoppingCartDTO
            {
                Id = cart.Id,
                UserId = cart.UserId,
                Items = cart.ShoppingCartItems.Select(i => new ShoppingCartItemDTO
                {
                    Id = i.Id,
                    ProductID = i.ProductID,
                    Quantity = i.Quantity,
                    ProductName = i.Product?.Name,
                    ProductPrice = i.Product?.Price ?? 0
                }).ToList()
            };
        }

        public async Task<int> CreateShoppingCartAsync(ShoppingCartDTO shoppingCartDto)
        {
            var cart = new ShoppingCart
            {
                UserId = shoppingCartDto.UserId,
                ShoppingCartItems = shoppingCartDto.Items.Select(i => new ShoppingCartItem
                {
                    ProductID = i.ProductID,
                    Quantity = i.Quantity
                }).ToList()
            };

            var created = await _shoppingCartRepository.AddAsync(cart);
            return created?.Id ?? 0;
        }

        public async Task<bool> UpdateShoppingCartAsync(int id, ShoppingCartDTO shoppingCartDto)
        {
            var existing = await _shoppingCartRepository.GetById(id);
            if (existing == null) return false;

            existing.UserId = shoppingCartDto.UserId;
            existing.ShoppingCartItems = shoppingCartDto.Items.Select(i => new ShoppingCartItem
            {
                Id = i.Id,
                ProductID = i.ProductID,
                Quantity = i.Quantity,
                ShoppingCartID = id
            }).ToList();

            await _shoppingCartRepository.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteShoppingCartAsync(int id)
        {
            var cart = await _shoppingCartRepository.GetById(id);
            if (cart == null) return false;

            return await _shoppingCartRepository.RemoveAsync(cart);
        }
    }
}