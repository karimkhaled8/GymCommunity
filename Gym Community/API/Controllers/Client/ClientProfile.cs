using Gym_Community.Application.Interfaces.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Gym_Community.API.Controllers.Client
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientProfile : ControllerBase
    {
        private readonly IClientProfileService _clientProfileService;
        public ClientProfile(IClientProfileService clientProfileService)
        {
            _clientProfileService = clientProfileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProfile([FromQuery] string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest(new { success = false, message = "User ID cannot be null or empty" });

            var clientProfile = await _clientProfileService.GetClientProfileByUserIdAsync(id);
            if (clientProfile == null)
                return NotFound(new { success = false, message = "Client profile not found" });

            return Ok(new { success = true, IsOwner = false, data = clientProfile });
        }

        [Authorize(Roles = "Client")]
        [HttpGet("me")]
        public async Task<IActionResult> GetMyProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrWhiteSpace(userId))
                return Unauthorized(new { success = false, message = "User ID is missing or invalid" });

            var clientProfile = await _clientProfileService.GetClientProfileByUserIdAsync(userId);
            if (clientProfile == null)
                return NotFound(new { success = false, message = "Profile not found" });

            return Ok(new { success = true, IsOwner = true, data = clientProfile });
        }


        [Authorize(Roles = "Client")]
        [HttpPut]
        public async Task<IActionResult> UpdateProfile([FromBody] API.DTOs.Client.UpdateClientProfileDTO clientInfoDTO)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrWhiteSpace(userId))
                return Unauthorized(new { success = false, message = "User ID is missing or invalid" });
            var result = await _clientProfileService.UpdateClientProfileAsync(clientInfoDTO, userId);
            if (!result)
                return BadRequest(new { success = false, message = "Failed to update profile" });
            return Ok(new { success = true, message = "Profile updated successfully" });
        }
    }
}
