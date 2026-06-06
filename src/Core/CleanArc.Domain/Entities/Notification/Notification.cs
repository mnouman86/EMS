using System;

namespace CleanArc.Domain.Entities.Notification
{
    public enum NotificationChannel
    {
        Whatsapp = 1,
        Sms = 2,
        Email = 3,
        InApp = 4
    }

    public enum NotificationStatus
    {
        Pending = 1,
        Sent = 2,
        Failed = 3
    }

    public class NotificationLog
    {
        public long Id { get; set; }
        public NotificationChannel Channel { get; set; }
        public string? Recipient { get; set; }            // phone / email / userId
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public string? TemplateKey { get; set; }          // e.g. "FeeReminder"
        public string? RelatedEntity { get; set; }        // e.g. "Invoice:123" / "Student:45"
        public NotificationStatus Status { get; set; }
        public string? FailureReason { get; set; }
        public int? SentBy { get; set; }                  // user id who triggered (null = system)
        public DateTime CreatedAt { get; set; }
        public DateTime? SentAt { get; set; }
    }
}
