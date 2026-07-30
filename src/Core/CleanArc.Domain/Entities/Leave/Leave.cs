using System;
using System.Collections.Generic;

namespace CleanArc.Domain.Entities.Leave
{
    public class LeaveType
    {
        public int LeaveTypeId { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public decimal DefaultAnnualQuota { get; set; }
        public bool IsPaid { get; set; }
        public bool RequiresAttachment { get; set; }
        public bool IsActive { get; set; }
        public int DisplayOrder { get; set; }
    }

    public class LeavePolicyRow
    {
        public int LeavePolicyId { get; set; }
        public int EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public int LeaveTypeId { get; set; }
        public string? LeaveTypeName { get; set; }
        public string? LeaveTypeCode { get; set; }
        public int AcademicYearId { get; set; }
        public string? AcademicYearName { get; set; }
        public decimal AnnualQuota { get; set; }
        public string? Notes { get; set; }
    }

    public class LeaveApprovalRouteRow
    {
        public int LeaveApprovalRouteId { get; set; }
        public int ApplicantRoleId { get; set; }
        public string? ApplicantRoleName { get; set; }
        public int ApproverUserId { get; set; }
        public string? ApproverName { get; set; }
        public string? ApproverUserName { get; set; }
        public string? Notes { get; set; }
    }

    public class LeaveBalanceRow
    {
        public int LeaveTypeId { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public bool IsPaid { get; set; }
        public bool RequiresAttachment { get; set; }
        public decimal Allocated { get; set; }
        public decimal Used { get; set; }
        public decimal Pending { get; set; }
        public decimal Available { get; set; }
    }

    public class LeaveApplicationRow
    {
        public int LeaveApplicationId { get; set; }
        public string? LeaveCode { get; set; }
        public int ApplicantUserId { get; set; }
        public string? ApplicantName { get; set; }
        public string? ApplicantUserName { get; set; }
        public int LeaveTypeId { get; set; }
        public string? LeaveTypeName { get; set; }
        public string? LeaveTypeCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool HalfDayFrom { get; set; }
        public bool HalfDayTo { get; set; }
        public decimal TotalDays { get; set; }
        public string? Reason { get; set; }
        public string? AttachmentPath { get; set; }
        public string? AttachmentOriginalName { get; set; }
        public string? Status { get; set; }
        public int? ApproverUserId { get; set; }
        public string? ApproverName { get; set; }
        public int? DecidedByUserId { get; set; }
        public string? DecidedByName { get; set; }
        public DateTime? DecidedAt { get; set; }
        public string? DecisionReason { get; set; }
        public DateTime SubmittedAt { get; set; }
    }

    public class LeaveDashboardTotals
    {
        public int Pending { get; set; }
        public int ApprovedThisMonth { get; set; }
        public int RejectedThisMonth { get; set; }
        public int UpcomingIn30Days { get; set; }
    }

    public class LeavePendingByType
    {
        public string? LeaveTypeName { get; set; }
        public int PendingCount { get; set; }
        public decimal PendingDays { get; set; }
    }

    public class LeaveRecentActivity
    {
        public int LeaveApplicationId { get; set; }
        public string? Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalDays { get; set; }
        public string? ApplicantName { get; set; }
        public string? LeaveTypeName { get; set; }
        public DateTime SubmittedAt { get; set; }
    }

    public class LeaveDashboardBundle
    {
        public LeaveDashboardTotals? Totals { get; set; }
        public List<LeavePendingByType> PendingByType { get; set; } = new();
        public List<LeaveRecentActivity> Recent { get; set; } = new();
    }
}
