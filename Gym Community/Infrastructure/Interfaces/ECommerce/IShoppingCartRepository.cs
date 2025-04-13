using Gym_Community.Domain.Data.Models.E_comm;

namespace Gym_Community.Infrastructure.Interfaces.ECommerce
{
    public interface IShoppingCartRepository
    {
        public Task<ShoppingCart?> AddAsync(ShoppingCart shoppingCart);
        public Task<IEnumerable<ShoppingCart>> ListAsync();
        public Task<ShoppingCart?> GetById(int id);
        public Task<ShoppingCart?> UpdateAsync(ShoppingCart shoppingCart);
        public Task<bool> RemoveAsync(ShoppingCart shoppingCart);
    }
}
