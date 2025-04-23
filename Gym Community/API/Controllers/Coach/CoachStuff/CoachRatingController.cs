using Gym_Community.API.DTOs.Coach.CoachStuff;
using Gym_Community.Application.Interfaces.CoachStuff;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Community.API.Controllers.Coach.CoachStuff
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoachRatingController : ControllerBase
    {
        private readonly ICoachRatingService _service;

        public CoachRatingController(ICoachRatingService service)
        {
            _service = service;
        }

        [HttpGet("byCoach/{coachId}")]
        public async Task<IActionResult> GetByCoachId(string coachId)
        {
            var result = await _service.GetByCoachIdAsync(coachId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CoachRatingDto dto)
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
