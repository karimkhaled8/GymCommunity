using Gym_Community.Infrastructure.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Gym_Community.Application.Services.Notification
{
    public class NotificationHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

        // Method to send notification to a specific user
        public async Task SendNotificationToUser(string userId, string message)
        {
            Console.WriteLine($"Sending notification to user {userId}: {message}");
            // Send notification to specific user
            await Clients.User(userId).SendAsync("ReceiveNotification", message);
        }
    }
}