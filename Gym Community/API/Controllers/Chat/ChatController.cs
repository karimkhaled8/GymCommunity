using Gym_Community.Application.Interfaces;
using Gym_Community.Application.Interfaces.Client;
using Gym_Community.Application.Services.Chat;
using Gym_Community.Domain.Models.Chat;
using Gym_Community.Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Gym_Community.API.Controllers.Client
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<ChatHub> _hubContext;
        public ChatController(ApplicationDbContext context , IHubContext<ChatHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
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

        [HttpPost("send")]
        [Authorize]
        public async Task<ActionResult<ChatMessage>> SendMessage([FromBody] SendMessageDto messageDto)
        {
            // Get the authenticated user's UserId from claims
            var senderId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(senderId))
            {
                return Unauthorized("User is not authenticated.");
            }

            // Validate input
            if (string.IsNullOrWhiteSpace(messageDto.GroupId) || string.IsNullOrWhiteSpace(messageDto.Content))
            {
                return BadRequest("Group ID and message content are required.");
            }

            // Verify the group exists
            var groupExists = await _context.ChatGroups.AnyAsync(g => g.GroupId == messageDto.GroupId);
            if (!groupExists)
            {
                return BadRequest("The specified group does not exist.");
            }

            // Verify the sender is a member of the group
            var isMember = await _context.GroupMembers
                .AnyAsync(m => m.GroupId == messageDto.GroupId && m.UserId == senderId);
            if (!isMember)
            {
                return Forbid("You are not a member of this group.");
            }

            // Create the message
            var message = new ChatMessage
            {
                SenderId = senderId,
                GroupId = messageDto.GroupId,
                Content = messageDto.Content,
                Timestamp = DateTime.UtcNow
            };

            // Save the message to the database
            _context.ChatMessages.Add(message);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "Failed to save the message.");
            }

            // Broadcast the message to group members via SignalR after saving
            try
            {
                await _hubContext.Clients.Group(messageDto.GroupId)
                    .SendAsync("ReceiveMessage", senderId, message.Content, message.Timestamp);
            }
            catch (Exception ex)
            {
                // Log the error to verify if it fails
                Console.WriteLine($"SignalR error: {ex.Message}");
            }

            return Ok(message);
        }
    }

    public class SendMessageDto
    {
        public string GroupId { get; set; }
        public string Content { get; set; }
    }
}

