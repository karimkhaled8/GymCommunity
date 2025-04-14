using Gym_Community.Application.Interfaces.IE_comm;
using Gym_Community.Domain.Data.Models.E_comm;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gym_Community.API.Controllers.Ecommerce
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly IWishlistService _wishlistService;
        public WishListController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService; 
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = getUserID(); 
            if(string.IsNullOrEmpty(userId)) return Unauthorized();
            var wishlist = await _wishlistService.GetUserWishlistAsync(userId);
            return Ok(wishlist);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] int productId)
        {
            var userId = getUserID();
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var result = await _wishlistService.AddToWishlistAsync(userId, productId);

            return result ? Ok(new { success = true, message = "Product added to wishlist" }) 
                : BadRequest(new { success = false, message = "Failed to add product to wishlist" });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = getUserID();
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var success = await _wishlistService.RemoveFromWishlistAsync(id);
            if (!success)
            {
                return BadRequest("Product not found in wish list");
            }
            return Ok("Product removed from wish list");
        }

        private string getUserID()
        {
            return  User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
