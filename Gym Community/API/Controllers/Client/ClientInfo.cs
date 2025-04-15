using Gym_Community.Application.Interfaces.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Gym_Community.API.Controllers.Client
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Client")]
    public class ClientInfo : ControllerBase
    {
        private readonly IClientInfoService _clientInfoService;

        public ClientInfo(IClientInfoService clientInfoService)
        {
            _clientInfoService = clientInfoService;
        }

     
        [HttpGet]
        public async Task<IActionResult> GetClientInfo()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null) return Unauthorized();

            var clientInfo = await _clientInfoService.GetClientInfoByUserIdAsync(userId);

            if (clientInfo == null)
                return NotFound(new { success = false, message = "Client info not found" });

            return Ok(clientInfo);
        }

        
        [HttpPost]
        public async Task<IActionResult> AddClientInfo([FromBody] API.DTOs.Client.ClientInfoDTO clientInfo)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            clientInfo.ClientId = userId; 

            var result = await _clientInfoService.AddClientInfoAsync(clientInfo);

            if (!result)
                return BadRequest(new { success = false, message = "Client info not added" });

            return CreatedAtAction(nameof(GetClientInfo), new { }, clientInfo);
        }

        
        [HttpPut]
        public async Task<IActionResult> UpdateClientInfo([FromBody] API.DTOs.Client.ClientInfoDTO clientInfo)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var result = await _clientInfoService.UpdateClientInfoAsync(userId, clientInfo);

            if (!result)
                return NotFound(new { success = false, message = "Client info not found" });

            return Ok(clientInfo);
        }

       
        [HttpDelete]
        public async Task<IActionResult> DeleteClientInfo()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var result = await _clientInfoService.DeleteClientInfoAsync(userId);

            if (!result)
                return NotFound(new { success = false, message = "Client info not found" });

            return Ok(new { success = true, message = "Client info deleted successfully" });
        }
    }
}
