using Gym_Community.Domain.Data.Models.E_comm;

namespace Gym_Community.Infrastructure.Interfaces.ECommerce
{
    public interface IWishListRepository
    {
        public Task<Wishlist?> AddAsync(Wishlist wishlist);
        public Task<IEnumerable<Wishlist>> ListAsync();
        public Task<Wishlist?> GetById(int id);
        public Task<Wishlist?> UpdateAsync(Wishlist wishlist);
        public Task<bool> RemoveAsync(Wishlist wishlist);
    }
}
