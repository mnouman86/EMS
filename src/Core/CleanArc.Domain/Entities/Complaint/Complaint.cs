using System;

namespace CleanArc.Domain.Entities.Complaint
{
    public class ComplaintNature
    {
        public int ComplaintNatureId { get; set; }
        public string? Name { get; set; }
        public string? ComplaintType { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public class ComplaintRow
    {
        public int ComplaintId { get; set; }
        public string? ComplaintCode { get; set; }
        public int ComplaintNatureId { get; set; }
        public string? NatureName { get; set; }
        public string? NatureType { get; set; }
        public int? LogonUserId { get; set; }
        public string? LogonUserFullName { get; set; }
        public string? ComplainantName { get; set; }
        public string? ContactNumber { get; set; }
        public string? ComplaintAgainst { get; set; }
        public string? Description { get; set; }
        public string? AttachmentPath { get; set; }
        public string? AttachmentOriginalName { get; set; }
        public string? Status { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? DeletedReason { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class ComplaintAuditEntry
    {
        public int ComplaintAuditId { get; set; }
        public int ComplaintId { get; set; }
        public string? Action { get; set; }
        public string? FromStatus { get; set; }
        public string? ToStatus { get; set; }
        public string? Note { get; set; }
        public int ActorUserId { get; set; }
        public string? ActorFullName { get; set; }
        public DateTime ActorAt { get; set; }
    }

    public class ComplaintDetail
    {
        public ComplaintRow? Header { get; set; }
        public System.Collections.Generic.List<ComplaintAuditEntry> AuditTrail { get; set; } = new();
    }
}
