using Gym_Community.API.DTOs.Gym;
using Gym_Community.Application.Interfaces.Gym;
using Gym_Community.Application.Services.Gym;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Community.API.Controllers.Gym
{
    [Route("api/[controller]")]
    [ApiController]
    public class GymController : ControllerBase
    {
        private readonly IGymService _service;

        public GymController(IGymService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GymReadDTO>>> ListAsync()
        {
            var gyms = await _service.ListAsync();
            return Ok(gyms);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GymReadDTO>> GetByIdAsync(int id)
        {
            var gym = await _service.GetByIdAsync(id);
            if (gym == null) return NotFound();
            return Ok(gym);
        }
        [HttpGet("ownerId/{ownerId}")]
        public async Task<ActionResult<GymReadDTO>> GetByOwnerIdAsync(string ownerId)
        {
            var gym = await _service.GetByOwnerIdAsync(ownerId);
            if (gym == null) return NotFound();
            return Ok(gym);
        }

        [HttpPost]
        public async Task<ActionResult<GymReadDTO>> AddAsync(GymCreateDTO dto)
        {
            var result = await _service.AddAsync(dto);
            return result != null ? Ok(result) : BadRequest("Failed to create gym.");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GymReadDTO>> UpdateAsync(int id, GymCreateDTO dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpGet("nearby")]
        public async Task<ActionResult<IEnumerable<GymReadDTO>>> GetNearbyGymsAsync(double lat, double lng, double radiusInKm)
        {
            var gyms = await _service.GetNearbyGymsAsync(lat, lng, radiusInKm);
            return Ok(gyms);
        }
    }
}
