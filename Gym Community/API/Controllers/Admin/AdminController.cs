using Gym_Community.Application.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;


namespace Gym_Community.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminDashboardService _dashboardService;

        public AdminController(IAdminDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetDashboardSummary()
        {
            var summary = await _dashboardService.GetSummaryAsync();
            return Ok(summary);
        }
        [HttpGet("users")]
        public async Task<IActionResult> GetUsersSummary(string role, int year)
        {
            var summary = await _dashboardService.GetMonthlyUserCountByRoleAsync(role,year);
            return Ok(summary);
        }
    }
}
