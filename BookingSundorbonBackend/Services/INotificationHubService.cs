namespace BookingSundorbonBackend.Services
{
    public interface INotificationHubService
    {
        Task SendToAllAsync(string method, object message);
        Task SendToGroupAsync(string groupName, string method, object message);
        Task SendToUserAsync(string userId, string method, object message);
    }
}
