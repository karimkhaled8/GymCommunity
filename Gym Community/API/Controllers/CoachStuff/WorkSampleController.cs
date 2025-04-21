using System.Security.Claims;
using Gym_Community.API.DTOs.CoachStuff;
using Gym_Community.Application.Interfaces;
using Gym_Community.Application.Interfaces.CoachStuff;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Community.API.Controllers.CoachStuff
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WorkSampleController : ControllerBase
    {
        private readonly IWorkSampleService _service;
        private readonly IAwsService _awsService;
        private readonly ICoachPortfolioService _portfolioService;

        public WorkSampleController(IWorkSampleService service , IAwsService awsService , ICoachPortfolioService portfolioService)
        {
            _service = service;
            _awsService = awsService;
            _portfolioService = portfolioService;
        }

        [HttpGet("byPortfolio/{portfolioId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByPortfolioId(int portfolioId)
        {
            var result = await _service.GetByPortfolioIdAsync(portfolioId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create( [FromForm] WorkSampleDto dto, [FromForm] IFormFile worksampleimg)
        {
            string imageUrl = string.Empty;
            if (worksampleimg != null)
            {
                imageUrl = await _awsService.UploadFileAsync(worksampleimg, "WorkSampleeImages");
            }

            var CoachId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (CoachId == null) return Unauthorized();

            dto.ProtofolioId = await _portfolioService.GetPortfolioIdByCoachIdAsync(CoachId);
            if (dto.ProtofolioId == 0) return BadRequest("No portfolio found for coach");

            dto.ImageUrl = imageUrl;

            var success = await _service.CreateAsync(dto);
            return success ? Ok("Work Sample Added") : BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? Ok() : NotFound();
        }
    }
}
