using Gym_Community.Application.Interfaces;
using Gym_Community.Application.Services;
using Gym_Community.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Gym_Community.API.Controllers.GeneralUser
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    
    public class GeneralUserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAuthService _authService;
        private readonly IAwsService _awsService;
        public GeneralUserController(UserManager<AppUser> userManager , IAuthService authService,IAwsService awsService)
        {
            _userManager = userManager;
            _authService = authService;
            _awsService = awsService;

        }


        [HttpPost("give-premium")]
        public IActionResult GivePremium()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _userManager.FindByIdAsync(userId).Result;
            if (user == null)
            {
                return NotFound("User not found");
            }
            if (user.IsPremium)
            {
                return BadRequest("User is already a premium member");
            }
            user.IsPremium = true;
            var result = _userManager.UpdateAsync(user).Result;

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update user");
            }

            var token = _authService.GenerateJwtTokenAsync(user);

            return Ok(new { success=true,message ="Congratulations you'r now a Premium Member!!",token = token });


        }


        [HttpPost("changeProfilePic")]
        public async Task<IActionResult> ChangeProfilePic([FromForm] IFormFile img)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }
            // Assuming you have a method to upload the file and get the URL

            string imageUrl = string.Empty;
            if (img != null)
            {
                imageUrl = await _awsService.UploadFileAsync(img, "ProfileImages");

            }
            user.ProfileImg = imageUrl;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update profile picture");
            }
            return Ok(new { success = true, message = "Profile picture updated successfully", imgUrl= user.ProfileImg });
        }


    }
}
