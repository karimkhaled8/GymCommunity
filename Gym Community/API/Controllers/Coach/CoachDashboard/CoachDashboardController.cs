using AutoMapper;
using Gym_Community.Infrastructure.Interfaces.CoachStuff;
using Gym_Community.Infrastructure.Interfaces.Training_Plans;
using Gym_Community.Infrastructure.Repositories.CoachStuff;
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
        private readonly ICoachDashboardRepository _coachDashboardRepository;

        public CoachDashboardController(
            ITrainingPlanRepository trainingPlanRepository,
            IWeekPlanRepository weekPlanRepository,
             ICoachDashboardRepository coachDashboardRepository
            )
        {
            _trainingPlanRepository = trainingPlanRepository;
            _weekPlanRepository = weekPlanRepository;
            _coachDashboardRepository = coachDashboardRepository;
        }
        [HttpGet("GetCoachDashboard")]
        [Authorize(Roles = "Coach")]
        public async Task<IActionResult> GetDashboardData(int yr,int month, DashboardTimeFilter filter)
        {
            try
            {
                var coachId = GetUserId();
                var trainingPlans = await _trainingPlanRepository.GetAllIncWeeksAsync(coachId);

                var coachDashboardData = await _coachDashboardRepository.GetDashboardSummaryAsync(coachId,filter,yr,month);

                if (trainingPlans == null)
                {
                    return NotFound(new { message = "No dashboard data found for this coach." });
                }
                return Ok(new {tplans=trainingPlans ,data = coachDashboardData });
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
