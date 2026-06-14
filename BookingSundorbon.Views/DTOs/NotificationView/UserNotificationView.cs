namespace BookingSundorbon.Views.DTOs.NotificationView
{
    public class UserNotificationView
    {
        public long RecipientId { get; set; }
        public long NotificationId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string? NotificationType { get; set; }
        public long? ReferenceId { get; set; }
        public string? ReferenceType { get; set; }
        public string? RedirectUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public long? CreatedBy { get; set; }
        public bool IsRead { get; set; }
        public DateTime? ReadAt { get; set; }
    }
}
