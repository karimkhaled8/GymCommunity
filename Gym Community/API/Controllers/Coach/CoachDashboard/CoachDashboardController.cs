using AutoMapper;
using Gym_Community.Infrastructure.Interfaces.Training_Plans;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Gym_Community.API.Controllers.Coach.CoachDashboard
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoachDashboardController : ControllerBase
    {

        private readonly ITrainingPlanRepository _trainingPlanRepository;
        private readonly IWeekPlanRepository _weekPlanRepository;
        private readonly IMapper _mapper;
        public CoachDashboardController(
            ITrainingPlanRepository trainingPlanRepository,
            IMapper mapper,
            IWeekPlanRepository weekPlanRepository)
        {
            _trainingPlanRepository = trainingPlanRepository;
            _mapper = mapper;
            _weekPlanRepository = weekPlanRepository;
        }
        [HttpGet("GetCoachDashboard")]
        [Authorize(Roles = "Coach")]
        public async Task<IActionResult> GetDashboardData()
        {
            try
            {
                var coachId = GetUserId();
                var trainingPlans = await _trainingPlanRepository.GetAllIncWeeksAsync(coachId);
                if (trainingPlans == null)
                {
                    return NotFound(new { message = "No dashboard data found for this coach." });
                }
                return Ok(new {tplans=trainingPlans , });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
        private string GetUserId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException("User ID not found in token");
            }
            return userId;
        }
    }
}
