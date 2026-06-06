using System;

namespace CleanArc.Domain.Entities.Result
{
    public class ResultSession
    {
        public int Id { get; set; }
        public string? Name { get; set; }                  // e.g. "2026 — Term 1"
        public string? Status { get; set; }                // Open / Locked
        public int MaxWritten { get; set; }                // default 100
        public int MaxOral { get; set; }                   // default 50
        public int MaxAttribute { get; set; }              // default 10 (each of 5 attrs)
        public decimal WeightWritten { get; set; }         // default 60
        public decimal WeightOral { get; set; }            // default 30
        public decimal WeightPerformance { get; set; }     // default 10
        public int RoundingDecimals { get; set; }          // default 2
        public bool? IsConfigLocked { get; set; }          // becomes true once any entry exists
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class GradeBand
    {
        public int Id { get; set; }
        public string? Grade { get; set; }                 // A+ / A / B / ...
        public decimal MinPercent { get; set; }
        public decimal MaxPercent { get; set; }
        public string? RemarkTemplate { get; set; }
        public int DisplayOrder { get; set; }
    }

    public class ResultEntry
    {
        public int Id { get; set; }
        public int ResultSessionId { get; set; }
        public int StudentId { get; set; }
        public int SchoolClassId { get; set; }
        public int SubjectId { get; set; }
        public int? TeacherEmployeeId { get; set; }

        public decimal? Written { get; set; }
        public decimal? Oral { get; set; }
        public decimal? AttrPunctuality { get; set; }
        public decimal? AttrDiscipline { get; set; }
        public decimal? AttrClassParticipation { get; set; }
        public decimal? AttrCreativity { get; set; }
        public decimal? AttrBehaviorWithPeers { get; set; }

        public decimal? PerSubjectPercent { get; set; }
        public string? Grade { get; set; }
        public string? Remark { get; set; }

        public string? SubjectNameSnapshot { get; set; }   // populated on lock (RES-05)

        public bool? IsLocked { get; set; }
        public DateTime? LockedAt { get; set; }
        public int? LockedBy { get; set; }

        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class ResultLockAudit
    {
        public int Id { get; set; }
        public int ResultSessionId { get; set; }
        public int SchoolClassId { get; set; }
        public string? Action { get; set; }                // Lock / Unlock
        public string? Reason { get; set; }
        public int ActorUserId { get; set; }
        public DateTime At { get; set; }
    }

    // ---------- query projections (returned by repo) ----------

    public class StudentSubjectMark
    {
        public int StudentId { get; set; }
        public string? StudentCode { get; set; }
        public string? StudentFullName { get; set; }
        public int SubjectId { get; set; }
        public string? SubjectName { get; set; }
        public bool IsRTL { get; set; }
        public decimal? Written { get; set; }
        public decimal? Oral { get; set; }
        public decimal? AttrPunctuality { get; set; }
        public decimal? AttrDiscipline { get; set; }
        public decimal? AttrClassParticipation { get; set; }
        public decimal? AttrCreativity { get; set; }
        public decimal? AttrBehaviorWithPeers { get; set; }
        public decimal? PerSubjectPercent { get; set; }
        public string? Grade { get; set; }
        public bool? IsLocked { get; set; }
    }

    public class ClassSheetRow
    {
        public int StudentId { get; set; }
        public string? StudentCode { get; set; }
        public string? StudentFullName { get; set; }
        public decimal? OverallPercent { get; set; }
        public string? OverallGrade { get; set; }
        public string? Remark { get; set; }
    }

    public class StudentResultCard
    {
        public int StudentId { get; set; }
        public string? StudentCode { get; set; }
        public string? StudentFullName { get; set; }
        public int ResultSessionId { get; set; }
        public string? SessionName { get; set; }
        public string? ClassName { get; set; }
        public decimal? OverallPercent { get; set; }
        public string? OverallGrade { get; set; }
        public string? Remark { get; set; }
        public DateTime IssuedAt { get; set; }
    }

    public class PreLockMissing
    {
        public int StudentId { get; set; }
        public string? StudentCode { get; set; }
        public string? StudentFullName { get; set; }
        public int SubjectId { get; set; }
        public string? SubjectName { get; set; }
        public string? Missing { get; set; }   // e.g. "Written, Oral, Attr*"
    }
}
