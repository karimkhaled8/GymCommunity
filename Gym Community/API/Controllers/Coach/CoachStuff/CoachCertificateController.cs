using System.Security.Claims;
using Gym_Community.API.DTOs.Coach.CoachStuff;
using Gym_Community.Application.Interfaces;
using Gym_Community.Application.Interfaces.CoachStuff;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Community.API.Controllers.CoachStuff
{
    [Route("api/[controller]")]
    [ApiController]

    public class CoachCertificateController : ControllerBase
    {
        private readonly ICoachCertificateService _service;
        private readonly IAwsService _awsService;
        private readonly ICoachPortfolioService _portfolioService;

        public CoachCertificateController(ICoachCertificateService service, IAwsService awsService, ICoachPortfolioService portfolioService)
        {
            _service = service;
            _awsService = awsService;
            _portfolioService = portfolioService;
        }

        [HttpGet("byPortfolio/{portfolioId}")]

        public async Task<IActionResult> GetByPortfolioId(int portfolioId)
        {
            //var CoachId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //if (CoachId == null) return Unauthorized();

            //portfolioId = await _portfolioService.GetPortfolioIdByCoachIdAsync(CoachId);
            //if (portfolioId == 0) return BadRequest("No portfolio found for coach");
            var result = await _service.GetByPortfolioIdAsync(portfolioId);
            return Ok(result);
        }

        [HttpGet("GetPortfolioIdByCoachId/{coachId}")]
        public async Task<IActionResult> GetPortfolioIdByCoachId(string coachId)
        {
            var portfolioId = await _portfolioService.GetPortfolioIdByCoachIdAsync(coachId);
            if (portfolioId == 0)
                return NotFound("No portfolio  found for this coach");

            return Ok(portfolioId);
        }


        [HttpPost("Create")]

        public async Task<IActionResult> Create([FromForm] CoachCertificateDto dto, [FromForm] IFormFile CertificateImage)
        {
            string imageUrl = string.Empty;
            if (CertificateImage != null)
            {
                imageUrl = await _awsService.UploadFileAsync(CertificateImage, "CertificateImages");
            }

            var CoachId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (CoachId == null) return Unauthorized();

            dto.ProtofolioId = await _portfolioService.GetPortfolioIdByCoachIdAsync(CoachId);
            if (dto.ProtofolioId == 0) return BadRequest("No portfolio found for coach");

            dto.ImageUrl = imageUrl;

            var success = await _service.CreateAsync(dto);
            return success
     ? Ok(new { message = "Certificate added" })
     : BadRequest(new { error = "Could not add certificate" });

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? Ok() : NotFound();
        }
    }
}