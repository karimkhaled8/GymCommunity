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
    public class CreateGroupDto
    {
        public string GroupName { get; set; }
        public string OtherUserId { get; set; }
    }
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
        [Authorize]
        public async Task<ActionResult<ChatGroup>> CreateGroup([FromBody] CreateGroupDto createGroupDto)
        {
            // Get the authenticated user's UserId from claims
            var creatorUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(creatorUserId))
            {
                return Unauthorized("User ID not found in claims.");
            }

            // Validate input
            if (string.IsNullOrWhiteSpace(createGroupDto.GroupName) || string.IsNullOrWhiteSpace(createGroupDto.OtherUserId))
            {
                return BadRequest("Group name and other user ID are required.");
            }

            // Create a new Group ID
            var newGroupId = Guid.NewGuid().ToString();

            // Create the new group
            var group = new ChatGroup
            {
                GroupId = newGroupId,
                GroupName = createGroupDto.GroupName
            };

            // Add the group to the database
            _context.ChatGroups.Add(group);

            // Add members to GroupMembers table
            var members = new List<GroupMember>
    {
        new GroupMember { UserId = creatorUserId, GroupId = newGroupId },
        new GroupMember { UserId = createGroupDto.OtherUserId, GroupId = newGroupId }
    };

            _context.GroupMembers.AddRange(members);

            await _context.SaveChangesAsync();

            // Load the group with members for the response
            var createdGroup = await _context.ChatGroups
                .Include(g => g.Members)
                .FirstOrDefaultAsync(g => g.GroupId == newGroupId);

            return Ok(createdGroup);
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

        [HttpGet("groups")]
        //[Authorize] // Optional: Uncomment to restrict to authenticated users
        public async Task<ActionResult<IEnumerable<ChatGroup>>> GetUserGroups()
        {
            // Get userId from the authenticated user's claims
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found in claims.");
            }

            // Get all groups the user is a member of, including other members
            var groups = await _context.GroupMembers
                .Where(m => m.UserId == userId)
                .Select(m => m.GroupId)
                .Distinct()
                .ToListAsync();

            var userGroups = await _context.ChatGroups
                .Where(g => groups.Contains(g.GroupId))
                .Include(g => g.Members)
                .ToListAsync();

            return Ok(userGroups);
        }

    }
}
