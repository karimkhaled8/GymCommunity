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
    public class GroupController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GroupController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<ChatGroup>> CreateGroup(ChatGroup group)
        {
            group.GroupId = Guid.NewGuid().ToString();
            _context.ChatGroups.Add(group);
            await _context.SaveChangesAsync();
            return Ok(group);
        }

        [HttpPost("{groupId}/members")]
        public async Task<ActionResult> AddMember(string groupId, [FromBody] string userId)
        {
            var member = new GroupMember { UserId = userId, GroupId = groupId };
            _context.GroupMembers.Add(member);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{groupId}/members/{userId}")]
        public async Task<ActionResult> RemoveMember(string groupId, string userId)
        {
            var member = await _context.GroupMembers
                .FirstOrDefaultAsync(m => m.GroupId == groupId && m.UserId == userId);
            if (member == null) return NotFound();
            _context.GroupMembers.Remove(member);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{userId}/groups")]
        public async Task<ActionResult<IEnumerable<ChatGroup>>> GetUserGroups(string userId)
        {
            var groups = await _context.GroupMembers
                .Where(m => m.UserId == userId)
                .Join(_context.ChatGroups,
                    m => m.GroupId,
                    g => g.GroupId,
                    (m, g) => g)
                .ToListAsync();
            return Ok(groups);
        }
    }
}
