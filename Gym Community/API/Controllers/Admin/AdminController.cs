using Gym_Community.API.DTOs.Auth;
using Gym_Community.Application.Interfaces;
using Gym_Community.Application.Interfaces.Admin;
using Gym_Community.Infrastructure.Interfaces.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace Gym_Community.API.Controllers.Admin
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminDashboardService _dashboardService;
        private readonly IAuthService _authService;
        private readonly IAdminDashboardRepository _dashboardRepository;

        public AdminController(
            IAdminDashboardService dashboardService
            ,IAuthService authService
            ,IAdminDashboardRepository dashboardRepository
            )
        {
            _dashboardService = dashboardService;
            _authService = authService;
            _dashboardRepository = dashboardRepository;
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


        [HttpPost]
        public async Task<IActionResult> CreateAdmin(RegisterDTO registerDTO, string profileImg)
        {
            if (ModelState.IsValid!) return BadRequest(ModelState);
            var userId = GetUserId();
            if (await _authService.GetRole(userId) != "Admin") return BadRequest("Not Authorized");
            var response  = await _authService.register(registerDTO, profileImg); 
            return Ok(response);
        }

        [HttpGet("admin/usermanagement")]
        public async Task<IActionResult> GetAllUsers(
           [FromQuery] string role = "Client",
           [FromQuery] string query = "",
           [FromQuery] bool? isActive = true,
           [FromQuery] bool? isPremium = false,
           [FromQuery] string gender = "all",
           [FromQuery] int pageNumber = 1,
           [FromQuery] int pageSize = 10)
        {
            var result = await _dashboardRepository.GetUsers(role, query, isActive, isPremium, gender, pageNumber, pageSize);
            return Ok(result);
        }

        private string GetUserId()
        {
            return User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        }

        

    }
}
