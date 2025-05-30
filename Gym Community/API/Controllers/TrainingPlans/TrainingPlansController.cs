﻿using AutoMapper;
using Gym_Community.API.Controllers.Client;
using Gym_Community.API.DTOs.TrainingPlanDtos;
using Gym_Community.Application.Interfaces.CoachStuff;
using Gym_Community.Domain.Data.Models.Meals_and_Exercise;
using Gym_Community.Domain.Models.Chat;
using Gym_Community.Domain.Models.Coach_Plans;
using Gym_Community.Infrastructure.Context;
using Gym_Community.Infrastructure.Interfaces.Training_Plans;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace Gym_Community.API.Controllers.TrainingPlans
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TrainingPlansController : ControllerBase
    {
        private readonly IDailyPlanRepository _dailyPlanRepository;
        private readonly IWeekPlanRepository _weekPlanRepository;
        private readonly ITrainingPlanRepository _trainingPlanRepository;
        private readonly IMapper _mapper;
        private readonly ICoachPortfolioService _CoachPortfolioService;
        private readonly ApplicationDbContext _context;

        public TrainingPlansController(
            IDailyPlanRepository dailyPlanRepository,
            IWeekPlanRepository weekPlanRepository,
            ITrainingPlanRepository trainingPlanRepository,
            IMapper mapper, ICoachPortfolioService CoachPortfolioService,
            ApplicationDbContext context
            )

        {
            _dailyPlanRepository = dailyPlanRepository;
            _weekPlanRepository = weekPlanRepository;
            _trainingPlanRepository = trainingPlanRepository;
            _mapper = mapper;
            _CoachPortfolioService = CoachPortfolioService;
            _context = context;
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

        private bool IsCoach()
        {
            return User.IsInRole("Coach");
        }

        private bool IsCoachAuthorized(string coachId)
        {
            return IsCoach() && GetUserId() == coachId;
        }

        // ===================== DAILY PLAN =====================

        [HttpGet("daily-plans")]
        public async Task<IActionResult> GetDailyPlansByWeekPlan([FromQuery] int weekPlanId)
        {
            try
            {
                var userId = GetUserId();
                var plans = await _dailyPlanRepository.GetByWeekIdAsync(weekPlanId, userId);
                var planDtos = _mapper.Map<IEnumerable<DailyPlanDto>>(plans);
                return Ok(planDtos);
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

        [HttpGet("daily-plans/{id}")]
        public async Task<IActionResult> GetDailyPlanById(int id)
        {
            try
            {
                var userId = GetUserId();
                var plan = await _dailyPlanRepository.GetByIdAsync(id, userId);
                if (plan == null)
                    return NotFound(new { message = "Daily plan not found or you don't have access to it" });

                var planDto = _mapper.Map<DailyPlanDto>(plan);
                return Ok(planDto);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the daily plan", error = ex.Message });
            }
        }

        [Authorize(Roles = "Coach")]
        [HttpPost("daily-plans")]
        public async Task<IActionResult> CreateDailyPlan([FromBody] CreateDailyPlanDto planDto)
        {
            try
            {
                var userId = GetUserId();

                var plan = _mapper.Map<DailyPlan>(planDto);
                await _dailyPlanRepository.AddAsync(plan);

                var createdPlanDto = _mapper.Map<DailyPlanDto>(plan);
                return CreatedAtAction(nameof(GetDailyPlanById), new { id = plan.Id }, createdPlanDto);
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

        [Authorize(Roles = "Coach")]
        [HttpPut("daily-plans/{id}")]
        public async Task<IActionResult> UpdateDailyPlan(int id, [FromBody] UpdateDailyPlanDto planDto)
        {
            try
            {
                var userId = GetUserId();

                if (id != planDto.Id)
                    return BadRequest(new { message = "ID in URL does not match ID in request body" });

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

        [Authorize(Roles = "Coach")]
        [HttpDelete("daily-plans/{id}")]
        public async Task<IActionResult> DeleteDailyPlan(int id)
        {
            try
            {
                var userId = GetUserId();

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


        [HttpPatch("markDailyPlan/{id}")]
        [Authorize(Roles ="Client")]
        public async Task<IActionResult> MarkDailyPlan(int id)
        {
            try
            {
                var userId = GetUserId();
                var dailyPlan = await _dailyPlanRepository.GetByIdAsync(id, userId);
                if (dailyPlan == null)
                    return NotFound(new { message = "Daily plan not found or you don't have access to it" });
                dailyPlan.IsDone = !dailyPlan.IsDone;
                await _dailyPlanRepository.UpdateAsync(dailyPlan);
                return Ok(new { message = "Day Marked" });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while marking the daily plan", error = ex.Message });
            }
        }


        // ===================== WEEK PLAN =====================


        [HttpGet("week-plans")]
        public async Task<IActionResult> GetWeekPlansByTrainingPlan([FromQuery] int trainingPlanId)
        {
            try
            {
                var userId = GetUserId();
                var plans = await _weekPlanRepository.GetByTrainingPlanIdAsync(trainingPlanId, userId);
                var planDtos = _mapper.Map<IEnumerable<WeekPlanDto>>(plans);
                return Ok(planDtos);
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

        [HttpGet("week-plans/{id}")]
        public async Task<IActionResult> GetWeekPlanById(int id)
        {
            try
            {
                var userId = GetUserId();
                var plan = await _weekPlanRepository.GetByIdAsync(id, userId);
                if (plan == null)
                    return NotFound(new { message = "Week plan not found or you don't have access to it" });

                var planDto = _mapper.Map<WeekPlanDto>(plan);
                return Ok(planDto);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the week plan", error = ex.Message });
            }
        }

        [Authorize(Roles = "Coach")]
        [HttpPost("week-plans")]
        public async Task<IActionResult> CreateWeekPlan([FromBody] CreateWeekPlanDto planDto)
        {
            try
            {
                var userId = GetUserId();
                if (!IsCoachAuthorized(userId))
                    return StatusCode(403, new { message = "Only the assigned coach can create week plans" });

                var plan = _mapper.Map<WeekPlan>(planDto);
                await _weekPlanRepository.AddAsync(plan, userId);

                var createdPlanDto = _mapper.Map<WeekPlanDto>(plan);
                return CreatedAtAction(nameof(GetWeekPlanById), new { id = plan.Id }, createdPlanDto);
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

        [Authorize(Roles = "Coach")]
        [HttpPut("week-plans/{id}")]
        public async Task<IActionResult> UpdateWeekPlan(int id, [FromBody] UpdateWeekPlanDto planDto)
        {
            try
            {
                var userId = GetUserId();

                if (id != planDto.Id)
                    return BadRequest(new { message = "ID in URL does not match ID in request body" });

                var existingPlan = await _weekPlanRepository.GetByIdAsync(id, userId);
                if (existingPlan == null)
                    return NotFound(new { message = "Week plan not found" });

                if (!IsCoachAuthorized(existingPlan.TrainingPlan.CoachId))
                    return StatusCode(403, new { message = "Only the assigned coach can update this week plan" });

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

        [Authorize(Roles = "Coach")]
        [HttpDelete("week-plans/{id}")]
        public async Task<IActionResult> DeleteWeekPlan(int id)
        {
            try
            {
                var userId = GetUserId();

                var plan = await _weekPlanRepository.GetByIdAsync(id, userId);
                if (plan == null)
                    return NotFound(new { message = "Week plan not found" });

                if (!IsCoachAuthorized(plan.TrainingPlan.CoachId))
                    return StatusCode(403, new { message = "Only the assigned coach can delete this week plan" });

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

        // ===================== TRAINING PLAN =====================
        //create training plan
        [HttpPost("Create")]
        [Authorize]
        public async Task<IActionResult> CreateTrainingPlan([FromBody] CreateTrainingPlanDto planDto)
        {
            try
            {
                var userId = GetUserId();
                //if (!IsCoachAuthorized(userId))
                //    return StatusCode(403, new { message = "Only the assigned coach can create training plans" });
                var plan = _mapper.Map<TrainingPlan>(planDto);
                //plan.CoachId = userId;
                //if (!IsCoach())
                //{
                //    plan.IsStaticPlan = true;
                //}
                await _trainingPlanRepository.AddAsync(plan, planDto.CoachId);
                var createdPlanDto = _mapper.Map<TrainingPlanDto>(plan);
                if(planDto.CoachId != null)
                {
                    var data = new
                    {
                        CoachId = planDto.CoachId,
                        ClientId = planDto.ClientId,
                        Name = planDto.Name,
                    };

                    string jsonString = JsonSerializer.Serialize(data);
                    await CreateTraingPlanChat(jsonString, planDto.CoachId, planDto.ClientId);
                }
               
                return CreatedAtAction(nameof(GetTrainingPlans), createdPlanDto);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the training plan", error = ex.Message });
            }
        }
        //get all training plans for user & coach
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetTrainingPlans()
        {
            var arry = new List<GetTrainingPlanDto>();

            try
            {
                var userId = GetUserId();
                var plans = await _trainingPlanRepository.GetAllAsync(userId);

                return Ok(plans);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving training plans", error = ex.Message });
            }
        }
        //get training plan by id
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetTrainingPlanById(int id)
        {
            try
            {
                var userId = GetUserId();
                var plan = await _trainingPlanRepository.GetByIdAsync(id, userId);
                if (plan == null)
                    return NotFound(new { message = "Training plan not found or you don't have access to it" });
                var planDto = _mapper.Map<TrainingPlanDto>(plan);

                planDto.Client = plan.Client;


                var coachPortofoilio = await _CoachPortfolioService.GetByCoachIdAsync(planDto.CoachId);

                return Ok(new { plan = planDto, coach = coachPortofoilio });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the training plan", error = ex.Message });
            }
        }
        private async         Task
CreateTraingPlanChat(string groupName ,string coachId , string clientId)
        {
            var newGroupId = Guid.NewGuid().ToString();

            var group = new ChatGroup
            {
                GroupId = newGroupId,
                GroupName = groupName
            };

            _context.ChatGroups.Add(group);

            var members = new List<GroupMember>
            {
                new GroupMember { UserId = coachId, GroupId = newGroupId },
                new GroupMember { UserId = clientId, GroupId = newGroupId }
            };

            _context.GroupMembers.AddRange(members);

            await _context.SaveChangesAsync();
        }

    }
}
