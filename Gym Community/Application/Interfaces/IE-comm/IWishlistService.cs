using Gym_Community.API.DTOs.E_comm;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gym_Community.Application.Interfaces.IE_comm
{
    public interface IWishlistService
    {
        Task<bool> AddToWishlistAsync(string userId, int productId);  // Changed return type
        Task<IEnumerable<WishlistDTO>> GetUserWishlistAsync(string userId);
        Task<bool> RemoveFromWishlistAsync(int wishlistId);
        Task<bool> IsProductInWishlistAsync(string userId, int productId);
    }
}