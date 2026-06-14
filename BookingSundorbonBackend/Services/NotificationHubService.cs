using BookingSundorbonBackend.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace BookingSundorbonBackend.Services
{
    public class NotificationHubService : INotificationHubService
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationHubService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public Task SendToAllAsync(string method, object message) =>
            _hubContext.Clients.All.SendAsync(method, message);

        public Task SendToGroupAsync(string groupName, string method, object message)
        {
            try
            {
                _hubContext.Clients.Group(groupName).SendAsync(method, message);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                return Task.FromException(ex);
                throw;
            }
       
        }


        public Task SendToUserAsync(string userId, string method, object message) =>
            _hubContext.Clients.User(userId).SendAsync(method, message);
    }
}
