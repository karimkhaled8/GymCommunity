using Gym_Community.API.DTOs.Forum;
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

        public PostController(IPostService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostCreateDTO dto)
        {
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
    }
}
