using System;

namespace CleanArc.Application.Models.Leave
{
    public class UpsertLeaveTypeDTO
    {
        public int? LeaveTypeId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal DefaultAnnualQuota { get; set; }
        public bool IsPaid { get; set; } = true;
        public bool RequiresAttachment { get; set; }
        public bool IsActive { get; set; } = true;
        public int DisplayOrder { get; set; } = 99;
    }

    public class UpsertLeavePolicyDTO
    {
        public int EmployeeId { get; set; }
        public int LeaveTypeId { get; set; }
        public int AcademicYearId { get; set; }
        public decimal AnnualQuota { get; set; }
        public string? Notes { get; set; }
    }

    public class UpsertLeaveRouteDTO
    {
        public int ApplicantRoleId { get; set; }
        public int ApproverUserId { get; set; }
        public string? Notes { get; set; }
    }

    public class SubmitLeaveApplicationDTO
    {
        public int ApplicantUserId { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool HalfDayFrom { get; set; }
        public bool HalfDayTo { get; set; }
        public string? Reason { get; set; }
        public string? AttachmentPath { get; set; }
        public string? AttachmentOriginalName { get; set; }
    }

    public class GetLeaveApplicationsFilter
    {
        public string Scope { get; set; } = "Mine"; // Mine | Pending | All
        public string? Status { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? LeaveTypeId { get; set; }
    }
}
