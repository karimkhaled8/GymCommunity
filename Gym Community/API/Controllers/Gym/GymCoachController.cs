using Gym_Community.API.DTOs.Gym;
using Gym_Community.Application.Interfaces.Gym;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Community.API.Controllers.Gym
{
    [Route("api/[controller]")]
    [ApiController]
    public class GymCoachController : ControllerBase
    {
        private readonly IGymCoachService _service;

        public GymCoachController(IGymCoachService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(GymCoachCreateDTO dto)
        {
            var result = await _service.CreateAsync(dto);
            return result != null ? Ok(result) : BadRequest("Failed to create GymCoach.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, GymCoachCreateDTO dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _service.DeleteAsync(id) ? Ok() : NotFound();
        }

        [HttpGet("gym/{gymId}")]
        public async Task<IActionResult> GetByGymId(int gymId)
        {
            var result = await _service.GetByGymIdAsync(gymId);
            return Ok(result);
        }
    }
}
