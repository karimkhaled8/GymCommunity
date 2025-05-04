using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Gym_Community.Application.Services.Notification
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        public string? GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
