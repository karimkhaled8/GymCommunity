using Gym_Community.Domain.Data.Models.E_comm;

namespace Gym_Community.Infrastructure.Interfaces.ECommerce
{
    public interface IShoppingCartItemRepository
    {
        public Task<ShoppingCartItem?> AddAsync(ShoppingCartItem shoppingCartItem);
        public Task<IEnumerable<ShoppingCartItem>> ListAsync();
        public Task<ShoppingCartItem?> GetById(int id);
        public Task<ShoppingCartItem?> UpdateAsync(ShoppingCartItem shoppingCartItem);
        public Task<bool> RemoveAsync(ShoppingCartItem shoppingCartItem);
    }
}
