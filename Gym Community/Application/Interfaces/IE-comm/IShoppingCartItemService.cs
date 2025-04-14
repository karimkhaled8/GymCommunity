using Gym_Community.API.DTOs.E_comm;

namespace Gym_Community.Application.Interfaces.IE_comm
{
    public interface IShoppingCartItemService
    {
        Task<IEnumerable<ShoppingCartItemDTO>> GetShoppingCartItems();
        Task<ShoppingCartItemDTO?> GetShoppingCartItemById(int id);
        Task<int> CreateShoppingCartItem(ShoppingCartItemDTO shoppingCartItemDto);
        Task<bool> UpdateShoppingCartItem(int id, ShoppingCartItemDTO shoppingCartItemDto);
        Task<bool> DeleteShoppingCartItem(int id);
    }
}
