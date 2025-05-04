using AutoMapper;
using Gym_Community.API.DTOs;
using Gym_Community.API.Mapping;
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
    //[Authorize]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IHubContext<NotificationHub> _hub;
        private readonly IMapper _mapper; 
        public NotificationController(
            INotificationRepository notificationRepository
           ,IHubContext<NotificationHub> hub
           , IMapper mapper
            )
        {
            _notificationRepository = notificationRepository;
            _hub = hub;
            _mapper = mapper; 
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
        public async Task<IActionResult> Add(NotificationDto notificationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var notification = _mapper.Map<Notification>(notificationDto);
            var createdNotification = await _notificationRepository.AddNotificationAsync(notification);

            if (createdNotification == null)
                return NotFound("Notification could not be created");

            // Send a real-time notification to the user via SignalR
            await _hub.Clients.All
             .SendAsync("ReceiveNotification", notificationDto.Body);


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
