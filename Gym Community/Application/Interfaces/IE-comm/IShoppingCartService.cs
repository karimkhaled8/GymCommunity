using Gym_Community.API.DTOs.E_comm;

namespace Gym_Community.Application.Interfaces.IE_comm
{
    public interface IShoppingCartService
    {
        Task<IEnumerable<ShoppingCartDTO>> GetShoppingCartsAsync();
        Task<ShoppingCartDTO?> GetShoppingCartByIdAsync(int id);
        Task<int> CreateShoppingCartAsync(ShoppingCartDTO shoppingCartDto);
        Task<bool> UpdateShoppingCartAsync(int id, ShoppingCartDTO shoppingCartDto);
        Task<bool> DeleteShoppingCartAsync(int id);
    }
}
