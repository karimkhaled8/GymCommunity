using System.ComponentModel.DataAnnotations.Schema;

namespace Gym_Community.API.DTOs
{
    public class NotificationDto
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool? isRead { get; set; } = false;
        public DateTime? CreatedAt { get; set; }
        public string? UserId { get; set; }
    }
}
