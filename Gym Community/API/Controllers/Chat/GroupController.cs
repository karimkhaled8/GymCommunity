using Gym_Community.Application.Interfaces;
using Gym_Community.Application.Interfaces.Client;
using Gym_Community.Domain.Models;
using Gym_Community.Domain.Models.Chat;
using Gym_Community.Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using AutoMapper.Execution;

namespace Gym_Community.API.Controllers.Client
{
    public class CreateGroupDto
    {
        public string GroupName { get; set; }
        public string OtherUserId { get; set; }
    }
    public class ChatGroupDto
    {
        public string GroupId { get; set; }

        public GroupNameDto GroupName { get; set; }

        public List<Member> Members { get; set; } = new();
    }

    public class GroupNameDto
    {
        public string CoachId { get; set; }
        public string ClientId { get; set; }
        public string Name { get; set; }
    }

    public class UserInfoDto
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string ProfileImage { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        // Constructor to inject dependencies
        public GroupController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Endpoint to create a new group
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ChatGroup>> CreateGroup([FromBody] CreateGroupDto createGroupDto)
        {
            var creatorUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(creatorUserId))
            {
                return Unauthorized("User ID not found in claims.");
            }

            if (string.IsNullOrWhiteSpace(createGroupDto.GroupName) || string.IsNullOrWhiteSpace(createGroupDto.OtherUserId))
            {
                return BadRequest("Group name and other user ID are required.");
            }

            var newGroupId = Guid.NewGuid().ToString();

            var group = new ChatGroup
            {
                GroupId = newGroupId,
                GroupName = createGroupDto.GroupName
            };

            _context.ChatGroups.Add(group);

            var members = new List<GroupMember>
            {
                new GroupMember { UserId = creatorUserId, GroupId = newGroupId },
                new GroupMember { UserId = createGroupDto.OtherUserId, GroupId = newGroupId }
            };

            _context.GroupMembers.AddRange(members);

            await _context.SaveChangesAsync();

            var createdGroup = await _context.ChatGroups
                .Include(g => g.Members)
                .FirstOrDefaultAsync(g => g.GroupId == newGroupId);

            return Ok(createdGroup);
        }

        // Endpoint to add a member to an existing group
        [HttpPost("{groupId}/members")]
        public async Task<ActionResult> AddMember(string groupId, [FromBody] string userId)
        {
            var member = new GroupMember { UserId = userId, GroupId = groupId };
            _context.GroupMembers.Add(member);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // Endpoint to remove a member from a group
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

        // Endpoint to get all groups of the authenticated user
        [HttpGet("groups")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ChatGroup>>> GetUserGroups()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found in claims.");
            }

            var groups = await _context.GroupMembers
                .Where(m => m.UserId == userId)
                .Select(m => m.GroupId)
                .Distinct()
                .ToListAsync();

            IEnumerable < ChatGroup >  userGroups = await _context.ChatGroups
                .Where(g => groups.Contains(g.GroupId))
                .Include(g => g.Members)
                .ToListAsync();

            return Ok(userGroups);
        }

        // Endpoint to get user information by ID
        [HttpGet("user/{id}")]
        public async Task<ActionResult<UserInfoDto>> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var userInfo = new UserInfoDto
            {
                UserId = user.Id,
                Name = user.FirstName,
                ProfileImage = user.ProfileImg
            };

            return Ok(userInfo);
        }
        [HttpGet("userName/{id}")]
       
        public async Task<ActionResult<string>> GetUserNameById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var userInfo = $"{user.FirstName} {user.LastName}";


            return Ok(userInfo);
        }
    }
}
