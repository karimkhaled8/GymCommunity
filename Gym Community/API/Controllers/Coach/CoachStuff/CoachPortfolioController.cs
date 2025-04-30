using Gym_Community.API.DTOs.Coach.CoachStuff;
using Gym_Community.Application.Interfaces;
using Gym_Community.Application.Interfaces.CoachStuff;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Gym_Community.API.Controllers.CoachStuff
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CoachPortfolioController : ControllerBase
    {
        private readonly ICoachPortfolioService _service;
        private readonly IAwsService _awsService;
        public CoachPortfolioController(ICoachPortfolioService service, IAwsService awsService)
        {
            _service = service;
            _awsService = awsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpGet("byCoach/{coachId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByCoachId(string coachId)
        {
            var item = await _service.GetByCoachIdAsync(coachId);
            return item == null ? NotFound() : Ok(item);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CoachPortfolioDto dto, [FromForm] IFormFile AboutMeImageUrl)
        {
            
            string imageUrl = string.Empty;
            if (AboutMeImageUrl != null)
            {
                imageUrl = await _awsService.UploadFileAsync(AboutMeImageUrl, "ProfileImages");
            }

            var coachId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (coachId == null) return Unauthorized();

            
            var existingPortfolio = await _service.GetByCoachIdAsync(coachId);
            if (existingPortfolio != null)
            {
                return BadRequest(new { message = "Portfolio already exists for this coach." });
            }

            
            dto.CoachId = coachId;
            dto.AboutMeImageUrl = imageUrl;

            var success = await _service.CreateAsync(dto);
            return success
                ? Ok(new { message = "Portfolio created successfully" })
                : BadRequest(new { message = "Failed to create portfolio" });
            }

            [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] CoachPortfolioDto dto, [FromForm] IFormFile? AboutMeImageUrl)
        {
            var coachId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (coachId == null) return Unauthorized();

            var existing = await _service.GetByCoachIdAsync(coachId);

            if (AboutMeImageUrl != null)
                dto.AboutMeImageUrl = await _awsService.UploadFileAsync(AboutMeImageUrl, "ProfileImages");

            dto.CoachId = coachId;

            if (existing == null)
            {
                var created = await _service.CreateAsync(dto);
                return created != null
                    ? Ok(new { message = "Portfolio created successfully", data = created })
                    : BadRequest(new { message = "Failed to create portfolio" });
            }
            else
            {
                var updated = await _service.UpdateAsync(existing.Id, dto);
                return updated
                    ? Ok(new { message = "Portfolio updated successfully" })
                    : BadRequest(new { message = "Failed to update portfolio" });
            }
        
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var coachId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var existing = await _service.GetByIdAsync(id);

            if (existing == null || existing.CoachId != coachId)
                return Unauthorized();

            var success = await _service.DeleteAsync(id);
            return success ? Ok() : NotFound();
        }
    }
}