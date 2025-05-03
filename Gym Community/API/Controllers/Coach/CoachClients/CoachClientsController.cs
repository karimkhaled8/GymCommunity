using AutoMapper;
using Gym_Community.API.DTOs.Coach.CoachClients;
using Gym_Community.Domain.Models;
using Gym_Community.Infrastructure.Interfaces.Training_Plans;
using Gym_Community.Infrastructure.Repositories.Training_Plans;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Gym_Community.API.Controllers.Coach.CoachClients
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoachClientsController : ControllerBase
    {
        private readonly ITrainingPlanRepository _trainingPlanRepository;
        private readonly IMapper _mapper;
        public CoachClientsController(
             
                ITrainingPlanRepository trainingPlanRepository,
                IMapper mapper)
        {
           
            _trainingPlanRepository = trainingPlanRepository;
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

        private bool IsCoach()
        {
            return User.IsInRole("Coach");
        }
        private bool IsCoachAuthorized(string coachId)
        {
            return IsCoach() && GetUserId() == coachId;
        }

        [HttpGet("GetAll")]
        [Authorize(Roles = "Coach")]
        public async Task<IActionResult> GetAllCoachClients([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var coachId = GetUserId();
                var plans = await _trainingPlanRepository.GetAllAsync(coachId);

                if (plans == null || !plans.Any())
                {
                    return NotFound(new { message = "No clients found for this coach." });
                }

                var clientDtos = plans
                    .Where(p => p.ClientId != null)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(p => new CoachClientsDTO
                    {
                        PlanId = p.Id,
                        PlanName = p.Name,
                        Client = p.Client
                    })
                    .ToList();

                var totalCount = plans.Count(p => p.ClientId != null);

                return Ok(new
                {
                    currentPage = pageNumber,
                    pageSize = pageSize,
                    totalCount = totalCount,
                    totalPages = (int)Math.Ceiling((double)totalCount / pageSize),
                    data = clientDtos
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving coach clients", error = ex.Message });
            }
        }



    }
}
