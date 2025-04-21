using Gym_Community.API.DTOs.Gym;
using Gym_Community.Application.Interfaces;
using Gym_Community.Application.Interfaces.Gym;
using Gym_Community.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Community.API.Controllers.Gym
{
    [Route("api/[controller]")]
    [ApiController]
    public class GymImgsController : ControllerBase
    {
        private readonly IGymImgService _service;
        private readonly IAwsService _awsService;


        public GymImgsController(IGymImgService service, IAwsService awsService)
        {
            _service = service;
            _awsService = awsService;

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]GymImgCreateDTO dto, [FromForm] IFormFile? image)
        {
            if (image != null)
            {
                var imageUrl = await _awsService.UploadFileAsync(image, "gymImage");
                if (string.IsNullOrEmpty(imageUrl))
                {
                    return BadRequest(new { success = false, message = "Failed to upload image" });
                }
                dto.ImageUrl = imageUrl;

            }
            var result = await _service.CreateAsync(dto);
            return result != null ? Ok(result) : BadRequest("Failed to create image.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet("gym/{gymId}")]
        public async Task<IActionResult> GetByGymId(int gymId)
        {
            var result = await _service.GetByGymIdAsync(gymId);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,[FromForm] GymImgCreateDTO dto, [FromForm] IFormFile? image)
        {
            var updated = await _service.GetByIdAsync(id);
            if(updated!=null)
                dto.ImageUrl = updated.ImageUrl;

            if (image != null)
            {
                if (!string.IsNullOrEmpty(dto.ImageUrl))
                    await _awsService.DeleteFileAsync(dto.ImageUrl);
                var imageUrl = await _awsService.UploadFileAsync(image, "gymImage");
                if (string.IsNullOrEmpty(imageUrl))
                {
                    return BadRequest(new { success = false, message = "Failed to upload image" });
                }
                dto.ImageUrl = imageUrl;

            }

            var result = await _service.UpdateAsync(id, dto);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.GetByIdAsync(id);
            if (deleted != null)
            {
                if (!string.IsNullOrEmpty(deleted.ImageUrl))
                    await _awsService.DeleteFileAsync(deleted.ImageUrl);

            }
            return await _service.DeleteAsync(id) ? Ok() : NotFound();
        }
    }
}
