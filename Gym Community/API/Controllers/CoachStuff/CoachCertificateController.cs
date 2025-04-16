using Gym_Community.API.DTOs.CoachStuff;
using Gym_Community.Application.Interfaces.CoachStuff;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Community.API.Controllers.CoachStuff
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoachCertificateController : ControllerBase
    {
        private readonly ICoachCertificateService _service;

        public CoachCertificateController(ICoachCertificateService service)
        {
            _service = service;
        }

        [HttpGet("byPortfolio/{portfolioId}")]
        public async Task<IActionResult> GetByPortfolioId(int portfolioId)
        {
            var result = await _service.GetByPortfolioIdAsync(portfolioId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CoachCertificateDto dto)
        {
            var success = await _service.CreateAsync(dto);
            return success ? Ok() : BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? Ok() : NotFound();
        }
    }
}
