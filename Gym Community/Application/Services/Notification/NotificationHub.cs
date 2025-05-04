using Gym_Community.Infrastructure.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Gym_Community.Application.Services.Notification
{
    public class NotificationHub : Hub
    {
        private readonly INotificationRepository _notificationRepository;
        public NotificationHub(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
