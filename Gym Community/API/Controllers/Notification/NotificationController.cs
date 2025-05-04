using Gym_Community.Application.Services.Notification;
using Gym_Community.Domain.Models.Notify;
using Gym_Community.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Sprache;
using System.Security.Claims;


namespace Gym_Community.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IHubContext<NotificationHub> _hub; 
        public NotificationController(INotificationRepository notificationRepository, IHubContext<NotificationHub> hub)
        {
            _notificationRepository = notificationRepository;
            _hub = hub;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserNorifications()
        {
            var userId = getUserId();
            if (userId == null) return BadRequest("user not authorized");
            var notifications = await _notificationRepository.GetUserNotificationsAsync(userId);
            return Ok(notifications);
        }
        [HttpGet("unread")]
        public async Task<IActionResult> GetUnreadNorifications()
        {
            var userId = getUserId();
            if (userId == null) return BadRequest("user not authorized");
            var notifications = await _notificationRepository.GetUnreadCountAsync(userId);
            return Ok(notifications);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotificationById(int id)
        {
            if (id <= 0) return BadRequest();
            var notification = await _notificationRepository.GetNotificationByIdAsync(id);
            return Ok(notification);
        }
        [HttpPost]
        public async Task<IActionResult> Add(Notification notification)
        {
            if (ModelState.IsValid!) return BadRequest(ModelState);
            notification.CreatedAt = DateTime.UtcNow;
            var userId = getUserId();
            if (userId == null) return BadRequest();
            var createdNotification = await _notificationRepository.AddNotificationAsync(notification);
            if (createdNotification == null) return NotFound();

            // Send real-time notification to the user
            await _hub.Clients.User(notification.UserId).SendAsync("ReceiveNotification", createdNotification);

            return Ok(createdNotification);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var result = await _notificationRepository.MarkAsReadAsync(id);
            if (!result) return NotFound();
            return Ok(new { sucess = result, Message = "Marked As Read" });
        }
        // DELETE: api/notification/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _notificationRepository.DeleteNotificationAsync(id);
            if (!result) return NotFound();
            return Ok("Notification deleted");
        }
        private string getUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

    }
}
