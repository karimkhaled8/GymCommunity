using Gym_Community.API.DTOs.CoachStuff;
using Gym_Community.Application.Interfaces.CoachStuff;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Community.API.Controllers.CoachStuff
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoachPortfolioController : ControllerBase
    {
        private readonly ICoachPortfolioService _service;

        public CoachPortfolioController(ICoachPortfolioService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpGet("byCoach/{coachId}")]
        public async Task<IActionResult> GetByCoachId(string coachId)
        {
            var item = await _service.GetByCoachIdAsync(coachId);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CoachPortfolioDto dto)
        {
            var success = await _service.CreateAsync(dto);
            return success ? Ok() : BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CoachPortfolioDto dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            return success ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? Ok() : NotFound();
        }
    }
}
