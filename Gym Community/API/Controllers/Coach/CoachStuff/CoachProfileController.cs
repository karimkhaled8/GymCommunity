using Gym_Community.API.DTOs.Coach.CoachStuff;
using Gym_Community.Application.Interfaces.CoachStuff;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Community.API.Controllers.Coach.CoachStuff
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoachProfileController : ControllerBase
    {
        private readonly ICoachPortfolioService _portfolioService;
        private readonly ICoachCertificateService _certificateService;
        private readonly IWorkSampleService _workSampleService;
        private readonly ICoachRatingService _ratingService;

        public CoachProfileController(ICoachPortfolioService portfolioService, ICoachCertificateService certificateService, IWorkSampleService workSampleService, ICoachRatingService ratingService)
        {
            _portfolioService = portfolioService;
            _certificateService = certificateService;
            _workSampleService = workSampleService;
            _ratingService = ratingService;
        }
    
    [HttpGet("fullProfile/{coachId}")]
        public async Task<IActionResult> GetFullProfile(string coachId)
        {
            var portfolio = await _portfolioService.GetByCoachIdAsync(coachId);
            var potfolioId = await _portfolioService.GetPortfolioIdByCoachIdAsync(coachId);
            if (portfolio == null) return NotFound("Portfolio not found");
            var certificates = await _certificateService.GetByPortfolioIdAsync(potfolioId);
            var workSamples = await _workSampleService.GetByPortfolioIdAsync(potfolioId);
            var ratings = await _ratingService.GetByCoachIdAsync(coachId);
            var fullProfile = new CoachFullProfileDto
            {
                Portfolio = portfolio,
                Certificates = certificates,
                WorkSamples = workSamples,
                Ratings = ratings
            };
            return Ok(fullProfile);
        }
    }
}
