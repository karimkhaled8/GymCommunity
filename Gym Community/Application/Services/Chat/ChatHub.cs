using Gym_Community.Domain.Models.Chat;
using Gym_Community.Infrastructure.Context;
using Microsoft.AspNetCore.SignalR;

namespace Gym_Community.Application.Services.Chat
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _context;

        public ChatHub(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SendMessageToGroup(string senderId, string groupId, string message)
        {
            var chatMessage = new ChatMessage
            {
                SenderId = senderId,
                GroupId = groupId,
                Content = message,
                Timestamp = DateTime.UtcNow
            };

            _context.ChatMessages.Add(chatMessage);
            await _context.SaveChangesAsync();

            // Send to all group members except the sender
            await Clients.OthersInGroup(groupId).SendAsync("ReceiveMessage", senderId, message, chatMessage.Timestamp);


        }
        public async Task JoinGroup(string groupId)
        {
            if (string.IsNullOrWhiteSpace(groupId))
            {
                throw new HubException("Group ID cannot be empty.");
            }
            await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
            await Clients.Caller.SendAsync("JoinedGroup", groupId); // Confirm to client
        }

        public async Task LeaveGroup(string groupId)
        {
            if (string.IsNullOrWhiteSpace(groupId))
            {
                throw new HubException("Group ID cannot be empty.");
            }
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupId);
        }
    } }

