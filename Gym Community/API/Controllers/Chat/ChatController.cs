using Gym_Community.Application.Interfaces;
using Gym_Community.Application.Interfaces.Client;
using Gym_Community.Domain.Models.Chat;
using Gym_Community.Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Gym_Community.API.Controllers.Client
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChatController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("history/{groupId}")]
        public async Task<ActionResult<IEnumerable<ChatMessage>>> GetGroupHistory(string groupId)
        {
            var messages = await _context.ChatMessages
                .Where(m => m.GroupId == groupId)
                .OrderBy(m => m.Timestamp)
                .ToListAsync();
            return Ok(messages);
        }
    }
}
