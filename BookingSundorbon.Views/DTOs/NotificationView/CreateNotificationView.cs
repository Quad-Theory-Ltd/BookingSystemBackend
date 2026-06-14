namespace BookingSundorbon.Views.DTOs.NotificationView
{
    public class CreateNotificationView
    {
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string? NotificationType { get; set; }
        public long? ReferenceId { get; set; }
        public string? ReferenceType { get; set; }
        public string? RedirectUrl { get; set; }
        public long? CreatedBy { get; set; }
        public List<string> RecipientUserIds { get; set; } = new();
    }
}
