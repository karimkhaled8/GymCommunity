using Google;
using Gym_Community.API.DTOs.Gym;
using Gym_Community.Application.Interfaces.Gym;
using Gym_Community.Application.Services.Gym;
using Gym_Community.Infrastructure.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gym_Community.API.Controllers.Gym
{
    [Route("api/[controller]")]
    [ApiController]
    public class GymController : ControllerBase
    {
        private readonly IGymService _service;

        public ApplicationDbContext _context { get; }

        public GymController(IGymService service , ApplicationDbContext applicationDbContext)
        {
            _service = service;
            _context = applicationDbContext;
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
        [HttpGet("all")]
        public async Task<IEnumerable<GymReadDTO>> GetAllAsync()
        {
            var gyms = await _context.Gym
                .Include(g => g.Images)
                .ToListAsync();

            var gymDTOs = gyms.Select(g => new GymReadDTO
            {
                Id = g.Id,
                Name = g.Name,
                Location = g.Location,
                OwnerId = g.OwnerId,
                Images = g.Images.Select(img => new GymImgDTO
                {
                    Id = img.Id,
                    ImageUrl = img.ImageUrl
                }).ToList()
            });

            return gymDTOs;
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateGymAsync(int id, [FromBody] GymUpdateDTO gymDto)
        {
            var gym = await _context.Gym.FindAsync(id);
            if (gym == null) return NotFound();

            gym.Name = gymDto.Name;
            gym.Location = gymDto.Location;
            // Add more fields if needed

            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteGymAsync(int id)
        {
            var gym = await _context.Gym.FindAsync(id);
            if (gym == null) return NotFound();

            _context.Gym.Remove(gym);
            await _context.SaveChangesAsync();

            return NoContent();
        }

      
       public class GymUpdateDTO
        {
            public string Name { get; set; } = null!;
            public string Location { get; set; } = null!;
            // 'OwnerId' is typically not updated in most cases
            public List<GymImgDTO> Images { get; set; } = new();
        }


        public class GymReadDTO
        {
            public int Id { get; set; }
            public string Name { get; set; } = null!;
            public string Location { get; set; } = null!;
            public string OwnerId { get; set; } = null!;

            public List<GymImgDTO> Images { get; set; } = new();
        }

        public class GymImgDTO
        {
            public int Id { get; set; }
            public string ImageUrl { get; set; } = null!;
        }

    }
}
