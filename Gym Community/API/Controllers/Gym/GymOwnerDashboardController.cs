using Gym_Community.Application.Interfaces.Gym;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Community.API.Controllers.Gym
{
    [Route("api/[controller]")]
    [ApiController]
    public class GymOwnerDashboardController : ControllerBase
    {
        IDashboardService _service;
        public GymOwnerDashboardController(IDashboardService service)
        {
            _service = service;
        }

        [HttpGet("GetSummary/{gymOwner}")]
        public async Task<IActionResult> GetSummary(string gymOwner)
        {
            var result = await _service.GetSummary(gymOwner);
            return Ok(result);
        }

        [HttpGet("GetTopPlans/{gymOwner}")]
        public async Task<IActionResult> GetTopPlans(string gymOwner)
        {
            var result = await _service.GetTopPlans(gymOwner);
            return Ok(result);
        }

        [HttpGet("GetRecentMembers/{gymOwner}")]
        public async Task<IActionResult> GetRecentMembers(string gymOwner)
        {
            var result = await _service.GetRecentMembers(gymOwner);
            return Ok(result);
        }
    }
}
