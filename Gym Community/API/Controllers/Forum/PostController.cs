using System.Security.Claims;
using Gym_Community.API.DTOs.E_comm;
using Gym_Community.API.DTOs.Forum;
using Gym_Community.Application.Interfaces;
using Gym_Community.Application.Interfaces.Forum;
using Gym_Community.Domain.Models.Forum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException("Please login first");
            }
            dto.UserId = userId;
            if (image != null)
            {
                var imageUrl = await _awsService.UploadFileAsync(image, "posts");
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
            return result != null ? Ok(result) : NotFound();
        }
        [HttpGet("topRated")]
        public async Task<IActionResult> GetTopRated()
        {
            var result = await _service.GetTopRated();
            return result != null ? Ok(result) : NotFound();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,[FromForm] PostCreateDTO dto, [FromForm] IFormFile? image)
        {
            var updated = await _service.GetByIdAsync(id);
            dto.ImgUrl = updated?.ImgUrl;
            if (image != null)
            {
                if (!string.IsNullOrEmpty(dto.ImgUrl))
                    await _awsService.DeleteFileAsync(dto.ImgUrl);
                var imageUrl = await _awsService.UploadFileAsync(image, "posts");
                if (string.IsNullOrEmpty(imageUrl))
                {
                    return BadRequest(new { success = false, message = "Failed to upload image" });
                }
                dto.ImgUrl = imageUrl;

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
                if (!string.IsNullOrEmpty(deleted.ImgUrl))
                    await _awsService.DeleteFileAsync(deleted.ImgUrl);

            }
            
            return await _service.DeleteAsync(id) ? Ok() : NotFound();
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(string userId)
        {
            var result = await _service.GetByUserIdAsync(userId);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet("sub/{subId}")]
        public async Task<IActionResult> GetBySub(int subId)
        {
            var result = await _service.GetBySubIdAsync(subId);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet("currentUserId")]
        public IActionResult GetUserId()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized(new
                    {
                        success = false,
                        message = "Please login first",
                        isAuthenticated = false
                    });
                }

                return Ok(new
                {
                    success = true,
                    userId = userId,
                    isAuthenticated = true
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "An error occurred while fetching user ID",
                    error = ex.Message
                });
            }
        }
    }
}
