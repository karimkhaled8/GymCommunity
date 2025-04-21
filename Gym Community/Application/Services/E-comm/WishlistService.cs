using Gym_Community.API.DTOs.E_comm;
using Gym_Community.Application.Interfaces.IE_comm;
using Gym_Community.Domain.Data.Models.E_comm;
using Gym_Community.Infrastructure.Interfaces.ECommerce;

namespace Gym_Community.Application.Services.E_comm
{
   public class WishlistService : IWishlistService
    {
            private readonly IWishlistRepository _wishlistRepository;

            public WishlistService(IWishlistRepository wishlistRepository)
            {
                _wishlistRepository = wishlistRepository;
            }

            public async Task<IEnumerable<WishlistDTO>> GetUserWishlistAsync(string userId)
            {
                if (string.IsNullOrEmpty(userId))
                {
                    throw new UnauthorizedAccessException("User is not authenticated or user ID is missing.");
                }

                var wishlistItems = await _wishlistRepository.GetByUserIdAsync(userId);

                return wishlistItems.Select(w => new WishlistDTO
                {
                    wishListId = w.Id,
                    Id = w.ProductID,
                    Name = w.Product.Name,
                    Description = w.Product.Description,
                    Price = w.Product.Price,
                    Stock = w.Product.Stock,
                    ImageUrl = w.Product.ImageUrl,
                    CreatedAt = w.CreatedAt,
                    AverageRating = w.Product.AverageRating,
                    CategoryID = w.Product.CategoryID,
                    CategoryName = w.Product.Category?.Name,
                    BrandId = w.Product.BrandId,
                    BrandName = w.Product.Brand?.Name
                }).ToList();
            }

            public async Task<bool> AddToWishlistAsync(string userId, int productId)
            {
                var existingItem = await _wishlistRepository.ProductExistsInWishlistAsync(userId, productId);
                if (existingItem)
                {
                    return false;
                }

                var wishlistItem = new Wishlist
                {
                    UserID = userId,
                    ProductID = productId,
                    CreatedAt = DateTime.Now
                };

                await _wishlistRepository.AddAsync(wishlistItem);
                return true;
            }

            public async Task<bool> RemoveFromWishlistAsync(int wishlistId)
            {
                var existingItem = await _wishlistRepository.GetByIdAsync(wishlistId);
                if (existingItem == null)
                {
                    return false;
                }

                return await _wishlistRepository.RemoveAsync(existingItem);
            }

            public async Task<bool> IsProductInWishlistAsync(string userId, int productId)
            {
                return await _wishlistRepository.ProductExistsInWishlistAsync(userId, productId);
            }
        
    }
}