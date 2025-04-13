using AutoMapper;
using Gym_Community.API.DTOs.TrainingPlanDtos;
using Gym_Community.Domain.Data.Models.Meals_and_Exercise;
using Gym_Community.Domain.Models.Coach_Plans;
using Gym_Community.Infrastructure.Interfaces.Training_Plans;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Gym_Community.API.Controllers.TrainingPlans
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TrainingPlansController : ControllerBase
    {
        private readonly IDailyPlanRepository _dailyPlanRepository;
        private readonly IWeekPlanRepository _weekPlanRepository;
        private readonly IMapper _mapper;

        public TrainingPlansController(
            IDailyPlanRepository dailyPlanRepository,
            IWeekPlanRepository weekPlanRepository,
            IMapper mapper)
        {
            _dailyPlanRepository = dailyPlanRepository;
            _weekPlanRepository = weekPlanRepository;
            _mapper = mapper;
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

        // ===================== DAILY PLAN =====================

        [HttpGet("daily-plans")]
        public async Task<IActionResult> GetDailyPlans([FromQuery] int? id = null, [FromQuery] int? weekPlanId = null)
        {
            try
            {
                var userId = GetUserId();

                if (id.HasValue)
                {
                    var plan = await _dailyPlanRepository.GetByIdAsync(id.Value, userId);
                    if (plan == null) 
                        return NotFound(new { message = "Daily plan not found or you don't have access to it" });
                    
                    var planDto = _mapper.Map<DailyPlanDto>(plan);
                    return Ok(planDto);
                }

                if (weekPlanId.HasValue)
                {
                    var plans = await _dailyPlanRepository.GetByWeekIdAsync(weekPlanId.Value, userId);
                    var planDtos = _mapper.Map<IEnumerable<DailyPlanDto>>(plans);
                    return Ok(planDtos);
                }

                var allPlans = await _dailyPlanRepository.GetAllAsync(userId);
                var allPlanDtos = _mapper.Map<IEnumerable<DailyPlanDto>>(allPlans);
                return Ok(allPlanDtos);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving daily plans", error = ex.Message });
            }
        }

        [HttpPost("daily-plans")]
        public async Task<IActionResult> CreateDailyPlan([FromBody] CreateDailyPlanDto planDto)
        {
            try
            {
                var userId = GetUserId();
                var plan = _mapper.Map<DailyPlan>(planDto);
                await _dailyPlanRepository.AddAsync(plan);
                
                var createdPlanDto = _mapper.Map<DailyPlanDto>(plan);
                return CreatedAtAction(nameof(GetDailyPlans), new { id = plan.Id }, createdPlanDto);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the daily plan", error = ex.Message });
            }
        }

        [HttpPut("daily-plans")]
        public async Task<IActionResult> UpdateDailyPlan([FromQuery] int id, [FromBody] UpdateDailyPlanDto planDto)
        {
            try
            {
                var userId = GetUserId();
                
                if (id != planDto.Id) 
                    return BadRequest(new { message = "ID in query parameter does not match ID in request body" });
                
                var isAuthorized = await _dailyPlanRepository.IsUserAuthorizedAsync(id, userId);
                if (!isAuthorized) 
                    return StatusCode(403, new { message = "You don't have permission to update this daily plan" });
                
                var existingPlan = await _dailyPlanRepository.GetByIdAsync(id, userId);
                if (existingPlan == null) 
                    return NotFound(new { message = "Daily plan not found" });
                
                _mapper.Map(planDto, existingPlan);
                await _dailyPlanRepository.UpdateAsync(existingPlan);
                
                return NoContent();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the daily plan", error = ex.Message });
            }
        }

        [HttpDelete("daily-plans")]
        public async Task<IActionResult> DeleteDailyPlan([FromQuery] int id)
        {
            try
            {
                var userId = GetUserId();
                
                var isAuthorized = await _dailyPlanRepository.IsUserAuthorizedAsync(id, userId);
                if (!isAuthorized) 
                    return StatusCode(403, new { message = "You don't have permission to delete this daily plan" });
                
                var plan = await _dailyPlanRepository.GetByIdAsync(id, userId);
                if (plan == null) 
                    return NotFound(new { message = "Daily plan not found" });

                await _dailyPlanRepository.DeleteAsync(plan);
                return NoContent();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the daily plan", error = ex.Message });
            }
        }

        // ===================== WEEK PLAN =====================

        [HttpGet("week-plans")]
        public async Task<IActionResult> GetWeekPlans([FromQuery] int? id = null, [FromQuery] int? trainingPlanId = null)
        {
            try
            {
                var userId = GetUserId();

                if (id.HasValue)
                {
                    var plan = await _weekPlanRepository.GetByIdAsync(id.Value, userId);
                    if (plan == null) 
                        return NotFound(new { message = "Week plan not found or you don't have access to it" });
                    
                    var planDto = _mapper.Map<WeekPlanDto>(plan);
                    return Ok(planDto);
                }

                if (trainingPlanId.HasValue)
                {
                    var plans = await _weekPlanRepository.GetByTrainingPlanIdAsync(trainingPlanId.Value, userId);
                    var planDtos = _mapper.Map<IEnumerable<WeekPlanDto>>(plans);
                    return Ok(planDtos);
                }

                var allPlans = await _weekPlanRepository.GetAllAsync(userId);
                var allPlanDtos = _mapper.Map<IEnumerable<WeekPlanDto>>(allPlans);
                return Ok(allPlanDtos);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving week plans", error = ex.Message });
            }
        }

        [HttpPost("week-plans")]
        public async Task<IActionResult> CreateWeekPlan([FromBody] CreateWeekPlanDto planDto)
        {
            try
            {
                var userId = GetUserId();
                var plan = _mapper.Map<WeekPlan>(planDto);
                await _weekPlanRepository.AddAsync(plan);
                
                var createdPlanDto = _mapper.Map<WeekPlanDto>(plan);
                return CreatedAtAction(nameof(GetWeekPlans), new { id = plan.Id }, createdPlanDto);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the week plan", error = ex.Message });
            }
        }

        [HttpPut("week-plans")]
        public async Task<IActionResult> UpdateWeekPlan([FromQuery] int id, [FromBody] UpdateWeekPlanDto planDto)
        {
            try
            {
                var userId = GetUserId();
                
                if (id != planDto.Id) 
                    return BadRequest(new { message = "ID in query parameter does not match ID in request body" });
                
                var isAuthorized = await _weekPlanRepository.IsUserAuthorizedAsync(id, userId);
                if (!isAuthorized) 
                    return StatusCode(403, new { message = "You don't have permission to update this week plan" });
                
                var existingPlan = await _weekPlanRepository.GetByIdAsync(id, userId);
                if (existingPlan == null) 
                    return NotFound(new { message = "Week plan not found" });
                
                _mapper.Map(planDto, existingPlan);
                await _weekPlanRepository.UpdateAsync(existingPlan);
                
                return NoContent();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the week plan", error = ex.Message });
            }
        }

        [HttpDelete("week-plans")]
        public async Task<IActionResult> DeleteWeekPlan([FromQuery] int id)
        {
            try
            {
                var userId = GetUserId();
                
                var isAuthorized = await _weekPlanRepository.IsUserAuthorizedAsync(id, userId);
                if (!isAuthorized) 
                    return StatusCode(403, new { message = "You don't have permission to delete this week plan" });
                
                var plan = await _weekPlanRepository.GetByIdAsync(id, userId);
                if (plan == null) 
                    return NotFound(new { message = "Week plan not found" });

                await _weekPlanRepository.DeleteAsync(plan);
                return NoContent();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the week plan", error = ex.Message });
            }
        }
    }
}
