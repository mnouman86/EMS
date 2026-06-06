using CleanArc.Domain.Entities.Notification;

namespace CleanArc.Application.Contracts.Notifications
{
    /// <summary>
    /// One message to be dispatched on a single channel.
    /// </summary>
    public class NotificationMessage
    {
        public NotificationChannel Channel { get; set; }
        public string Recipient { get; set; }           // phone / email / userId
        public string Subject { get; set; }             // used by Email / InApp; ignored elsewhere
        public string Body { get; set; }
        public string TemplateKey { get; set; }
        public string RelatedEntity { get; set; }
        public int? SentBy { get; set; }
    }

    public class NotificationSendResult
    {
        public bool Success { get; set; }
        public string FailureReason { get; set; }
        public long LogId { get; set; }
    }

    /// <summary>
    /// Dispatch facade. Implementation routes the message to the right channel
    /// provider (Twilio / SMTP / WhatsApp gateway / in-app store) and persists a
    /// <see cref="NotificationLog"/> row regardless of outcome.
    /// </summary>
    public interface INotificationSender
    {
        Task<NotificationSendResult> SendAsync(NotificationMessage message, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<NotificationSendResult>> SendBulkAsync(IEnumerable<NotificationMessage> messages, CancellationToken cancellationToken = default);
    }
}
