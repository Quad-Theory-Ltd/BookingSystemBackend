using BookingSundorbon.Views.DTOs.NotificationView;

namespace BookingSundorbon.Features.Repositories.NotificationRepository
{
    public interface INotificationRepository
    {
        Task<long> CreateNotificationAsync(CreateNotificationView notification);
        Task<IEnumerable<UserNotificationView>> GetUserNotificationsAsync(string userId, bool unreadOnly = false, int page = 1, int pageSize = 20);
        Task<int> GetUnreadCountAsync(string userId);
        Task<bool> MarkAsReadAsync(long recipientId, string userId);
        Task<bool> SoftDeleteAsync(long recipientId, string userId);
        Task<bool> MarkAllAsReadAsync(string userId);
    }
}
