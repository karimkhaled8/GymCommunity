using Gym_Community.Application.Interfaces;
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
        public GeneralUserController(UserManager<AppUser> userManager , IAuthService authService)
        {
            _userManager = userManager;
            _authService = authService;

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
    }
}
