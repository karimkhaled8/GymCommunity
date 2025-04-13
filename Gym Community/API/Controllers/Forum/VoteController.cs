using Gym_Community.API.DTOs.Forum;
using Gym_Community.Application.Interfaces.Forum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Community.API.Controllers.Forum
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        private readonly IVoteService _service;

        public VoteController(IVoteService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(VoteCreateDTO dto)
        {
            var result = await _service.CreateAsync(dto);
            return result != null ? Ok(result) : BadRequest("Failed to create vote.");
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
        public async Task<ActionResult<VoteReadDTO>> Update(int id, VoteCreateDTO voteUpdateDTO)
        {
            var updatedVote = await _service.UpdateAsync(id, voteUpdateDTO);
            return updatedVote == null ? NotFound() : Ok(updatedVote);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _service.DeleteAsync(id) ? Ok() : NotFound();
        }
        [HttpGet("post/{postId}")]
        public async Task<ActionResult<IEnumerable<VoteReadDTO>>> GetVotesByPostId(int postId)
        {
            var votes = await _service.GetVotesByPostIdAsync(postId);
            return Ok(votes);
        }
        [HttpGet("comment/{commentId}")]
        public async Task<ActionResult<IEnumerable<VoteReadDTO>>> GetVotesByCommentId(int commentId)
        {
            var votes = await _service.GetVotesByCommentIdAsync(commentId);
            return Ok(votes);
        }
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<VoteReadDTO>>> GetVotesByUserId(string userId)
        {
            var votes = await _service.GetVotesByUserIdAsync(userId);
            return Ok(votes);
        }

    }
}
