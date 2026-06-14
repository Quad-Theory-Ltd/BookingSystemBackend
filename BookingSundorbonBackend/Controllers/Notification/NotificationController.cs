using BookingSundorbon.Features.Repositories.NotificationRepository;
using BookingSundorbon.Views.DTOs.NotificationView;
using BookingSundorbonBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.Notification
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationHubService _notificationHubService;

        public NotificationController(
            INotificationRepository notificationRepository,
            INotificationHubService notificationHubService)
        {
            _notificationRepository = notificationRepository;
            _notificationHubService = notificationHubService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotification([FromBody] CreateNotificationView request)
        {
            if (request == null)
            {
                return BadRequest("Notification is null.");
            }

            if (string.IsNullOrWhiteSpace(request.Title) || string.IsNullOrWhiteSpace(request.Message))
            {
                return BadRequest("Title and Message are required.");
            }

            if (request.RecipientUserIds == null || request.RecipientUserIds.Count == 0)
            {
                return BadRequest("At least one recipient is required.");
            }

            var notificationId = await _notificationRepository.CreateNotificationAsync(request);

            foreach (var userId in request.RecipientUserIds.Distinct())
            {
                var payload = new UserNotificationView
                {
                    NotificationId = notificationId,
                    Title = request.Title,
                    Message = request.Message,
                    NotificationType = request.NotificationType,
                    ReferenceId = request.ReferenceId,
                    ReferenceType = request.ReferenceType,
                    RedirectUrl = request.RedirectUrl,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = request.CreatedBy,
                    IsRead = false
                };

                await _notificationHubService.SendToGroupAsync(
                    $"user_{userId}",
                    "NotificationReceived",
                    payload);
            }

            return Ok(new CreateNotificationResultView { NotificationId = notificationId });
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserNotifications(
            string userId,
            [FromQuery] bool unreadOnly = false,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            if (page < 1 || pageSize < 1)
            {
                return BadRequest("Page and pageSize must be greater than zero.");
            }

            var notifications = await _notificationRepository.GetUserNotificationsAsync(userId, unreadOnly, page, pageSize);
            return Ok(notifications);
        }

        [HttpGet("user/{userId}/unread-count")]
        public async Task<IActionResult> GetUnreadCount(string userId)
        {
            var count = await _notificationRepository.GetUnreadCountAsync(userId);
            return Ok(new { unreadCount = count });
        }

        [HttpPost("recipient/{recipientId:long}/read")]
        public async Task<IActionResult> MarkAsRead(long recipientId, [FromQuery] string userId)
        {
            var updated = await _notificationRepository.MarkAsReadAsync(recipientId, userId);
            if (!updated)
            {
                return NotFound("Notification not found.");
            }

            return Ok("Notification marked as read.");
        }

        [HttpPost("user/{userId}/read-all")]
        public async Task<IActionResult> MarkAllAsRead(string userId)
        {
            await _notificationRepository.MarkAllAsReadAsync(userId);
            return Ok("All notifications marked as read.");
        }

        [HttpDelete("recipient/{recipientId:long}")]
        public async Task<IActionResult> SoftDelete(long recipientId, [FromQuery] string userId)
        {
            var deleted = await _notificationRepository.SoftDeleteAsync(recipientId, userId);
            if (!deleted)
            {
                return NotFound("Notification not found.");
            }

            return Ok("Notification deleted.");
        }
    }
}
