using Gym_Community.API.DTOs.E_comm;
using Gym_Community.API.DTOs.Forum;
using Gym_Community.Application.Interfaces;
using Gym_Community.Application.Interfaces.Forum;
using Gym_Community.Domain.Models.Forum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Community.API.Controllers.Forum
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _service;
        private readonly IAwsService _awsService;
        public PostController(IPostService service, IAwsService awsService)
        {
            _service = service;
            _awsService = awsService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]PostCreateDTO dto,[FromForm] IFormFile? image)
        {
            if(image != null)
            {
                var imageUrl = await _awsService.UploadFileAsync(image, "products");
                if (string.IsNullOrEmpty(imageUrl))
                {
                    return BadRequest(new { success = false, message = "Failed to upload image" });
                }
                dto.ImgUrl = imageUrl;

            }

            var result = await _service.CreateAsync(dto);
            return result != null ? Ok(result) : BadRequest("Failed to create post.");
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
        public async Task<IActionResult> Update(int id, PostCreateDTO dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _service.DeleteAsync(id) ? Ok() : NotFound();
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(string userId)
        {
            var result = await _service.GetByUserIdAsync(userId);
            return Ok(result);
        }

        [HttpGet("sub/{subId}")]
        public async Task<IActionResult> GetBySub(int subId)
        {
            var result = await _service.GetBySubIdAsync(subId);
            return Ok(result);
        }

    }
}
