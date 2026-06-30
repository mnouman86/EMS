using System;

namespace CleanArc.Application.Models.Complaint
{
    public class UpsertComplaintNatureDTO
    {
        public int? ComplaintNatureId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ComplaintType { get; set; } = "Complaint"; // Complaint | Suggestion | Either
        public int DisplayOrder { get; set; } = 99;
        public bool IsActive { get; set; } = true;
    }

    public class CreateComplaintDTO
    {
        public int LogonUserId { get; set; }
        public int ComplaintNatureId { get; set; }
        public string ComplainantName { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public string? ComplaintAgainst { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? AttachmentPath { get; set; }
        public string? AttachmentOriginalName { get; set; }
    }

    public class UpdateComplaintDTO
    {
        public int ComplaintId { get; set; }
        public int ActorUserId { get; set; }
        public int ComplaintNatureId { get; set; }
        public string ComplainantName { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public string? ComplaintAgainst { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? AttachmentPath { get; set; }
        public string? AttachmentOriginalName { get; set; }
        public bool ClearAttachment { get; set; }
    }

    public class GetComplaintsReportDTO
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? ComplaintType { get; set; }
        public int? ComplaintNatureId { get; set; }
        public string? Status { get; set; }
        public string? ComplainantLike { get; set; }
        public int? LogonUserId { get; set; }
        public bool? HasAttachment { get; set; }
        public bool IncludeDeleted { get; set; }
    }
}
