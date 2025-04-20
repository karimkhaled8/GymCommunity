using System.Security.Claims;
using Gym_Community.API.DTOs.Forum;
using Gym_Community.Application.Interfaces.Forum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Community.API.Controllers.Forum
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _service;

        public CommentController(ICommentService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CommentCreateDTO dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException("Please login first");
            }
            dto.UserId = userId;
            var result = await _service.CreateAsync(dto);
            return result != null ? Ok(result) : BadRequest("Failed to create comment.");
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
        public async Task<IActionResult> Update(int id, CommentCreateDTO dto)
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
        public async Task<ActionResult<IEnumerable<CommentReadDTO>>> GetByUserId(string userId)
        {
            var comments = await _service.GetByUserIdAsync(userId);
            return Ok(comments);
        }

        [HttpGet("post/{postId}")]
        public async Task<ActionResult<IEnumerable<CommentReadDTO>>> GetByPostId(int postId)
        {
            var comments = await _service.GetByPostIdAsync(postId);
            return Ok(comments);
        }
        private string GetUserId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException("Please login first");
            }
            return userId;
        }
    }
}
