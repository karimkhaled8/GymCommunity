using Gym_Community.Domain.Data.Models.E_comm;

namespace Gym_Community.Infrastructure.Interfaces.ECommerce
{
    public interface IWishlistRepository
    {
        Task<Wishlist?> AddAsync(Wishlist wishlist);
        Task<IEnumerable<Wishlist>> ListAsync();
        Task<Wishlist?> GetByIdAsync(int id);
        Task<IEnumerable<Wishlist>> GetByUserIdAsync(string userId);
        Task<Wishlist?> UpdateAsync(Wishlist wishlist);
        Task<bool> RemoveAsync(Wishlist wishlist);
        Task<bool> ProductExistsInWishlistAsync(string userId, int productId);
    }
}
