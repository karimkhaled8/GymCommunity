using AutoMapper;
using Gym_Community.API.DTOs.TrainingPlanDtos;
using Gym_Community.Domain.Data.Models.Meals_and_Exercise;
using Gym_Community.Domain.Models.Coach_Plans;
using Gym_Community.Infrastructure.Interfaces.Training_Plans;
using Gym_Community.Infrastructure.Repositories.Training_Plans;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Community.API.Controllers.TrainingPlans
{
    [Route("api/[controller]")]
    [ApiController]
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

        // ===================== DAILY PLAN =====================

        [HttpGet("daily-plans")]
        public async Task<IActionResult> GetAllDailyPlans()
        {
            var plans = await _dailyPlanRepository.GetAllAsync();
            var planDtos = _mapper.Map<IEnumerable<DailyPlanDto>>(plans);
            return Ok(planDtos);
        }

        [HttpGet("daily-plans/{id}")]
        public async Task<IActionResult> GetDailyPlan(int id)
        {
            var plan = await _dailyPlanRepository.GetByIdAsync(id);
            if (plan == null) return NotFound();
            
            var planDto = _mapper.Map<DailyPlanDto>(plan);
            return Ok(planDto);
        }

        [HttpPost("daily-plans")]
        public async Task<IActionResult> CreateDailyPlan([FromBody] CreateDailyPlanDto planDto)
        {
            var plan = _mapper.Map<DailyPlan>(planDto);
            await _dailyPlanRepository.AddAsync(plan);
            
            var createdPlanDto = _mapper.Map<DailyPlanDto>(plan);
            return CreatedAtAction(nameof(GetDailyPlan), new { id = plan.Id }, createdPlanDto);
        }

        [HttpPut("daily-plans/{id}")]
        public async Task<IActionResult> UpdateDailyPlan(int id, [FromBody] UpdateDailyPlanDto planDto)
        {
            if (id != planDto.Id) return BadRequest();
            
            var existingPlan = await _dailyPlanRepository.GetByIdAsync(id);
            if (existingPlan == null) return NotFound();
            
            _mapper.Map(planDto, existingPlan);
            await _dailyPlanRepository.UpdateAsync(existingPlan);
            
            return NoContent();
        }

        [HttpDelete("daily-plans/{id}")]
        public async Task<IActionResult> DeleteDailyPlan(int id)
        {
            var plan = await _dailyPlanRepository.GetByIdAsync(id);
            if (plan == null) return NotFound();

            await _dailyPlanRepository.DeleteAsync(plan);
            return NoContent();
        }

        // ===================== WEEK PLAN =====================

        [HttpGet("week-plans")]
        public async Task<IActionResult> GetAllWeekPlans()
        {
            var plans = await _weekPlanRepository.GetAllAsync();
            var planDtos = _mapper.Map<IEnumerable<WeekPlanDto>>(plans);
            return Ok(planDtos);
        }

        [HttpGet("week-plans/{id}")]
        public async Task<IActionResult> GetWeekPlan(int id)
        {
            var plan = await _weekPlanRepository.GetByIdAsync(id);
            if (plan == null) return NotFound();
            
            var planDto = _mapper.Map<WeekPlanDto>(plan);
            return Ok(planDto);
        }

        [HttpPost("week-plans")]
        public async Task<IActionResult> CreateWeekPlan([FromBody] CreateWeekPlanDto planDto)
        {
            var plan = _mapper.Map<WeekPlan>(planDto);
            await _weekPlanRepository.AddAsync(plan);
            
            var createdPlanDto = _mapper.Map<WeekPlanDto>(plan);
            return CreatedAtAction(nameof(GetWeekPlan), new { id = plan.Id }, createdPlanDto);
        }

        [HttpPut("week-plans/{id}")]
        public async Task<IActionResult> UpdateWeekPlan(int id, [FromBody] UpdateWeekPlanDto planDto)
        {
            if (id != planDto.Id) return BadRequest();
            
            var existingPlan = await _weekPlanRepository.GetByIdAsync(id);
            if (existingPlan == null) return NotFound();
            
            _mapper.Map(planDto, existingPlan);
            await _weekPlanRepository.UpdateAsync(existingPlan);
            
            return NoContent();
        }

        [HttpDelete("week-plans/{id}")]
        public async Task<IActionResult> DeleteWeekPlan(int id)
        {
            var plan = await _weekPlanRepository.GetByIdAsync(id);
            if (plan == null) return NotFound();

            await _weekPlanRepository.DeleteAsync(plan);
            return NoContent();
        }
    }
}
