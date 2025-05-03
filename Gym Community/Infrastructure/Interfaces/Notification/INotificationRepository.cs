using Gym_Community.Domain.Models.Notify; 

namespace Gym_Community.Infrastructure.Interfaces
{
    public interface INotificationRepository
    {
        Task<IEnumerable<Notification>> GetUserNotificationsAsync(string userId);
        Task<Notification> GetNotificationByIdAsync(int id);
        Task<Notification> AddNotificationAsync(Notification notification);
        Task<bool> MarkAsReadAsync(int id);
        Task<bool> DeleteNotificationAsync(int id);
        Task<int> GetUnreadCountAsync(string userId);
    }
}
