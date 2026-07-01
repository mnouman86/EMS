using System;

namespace CleanArc.Domain.Entities.Attendance
{
    /// <summary>One row per student in the entry grid (may have no attendance recorded yet).</summary>
    public class ClassAttendanceGridRow
    {
        public int StudentId { get; set; }
        public string? StudentCode { get; set; }
        public string? FormNo { get; set; }
        public string? FullName { get; set; }
        public string? StudentStatus { get; set; }
        public int? AttendanceId { get; set; }
        public int WorkingDays { get; set; }
        public int PresentDays { get; set; }
        public string? Remarks { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    /// <summary>One row per recorded month for a student (parent / profile view).</summary>
    public class StudentAttendancePeriodRow
    {
        public int Id { get; set; }
        public int PeriodYear { get; set; }
        public int PeriodMonth { get; set; }
        public int WorkingDays { get; set; }
        public int PresentDays { get; set; }
        public int AbsentDays { get; set; }
        public decimal? AttendancePercent { get; set; }
        public string? Remarks { get; set; }
        public string? ClassName { get; set; }
        public string? AcademicYear { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    /// <summary>Aggregate across a year (or all time) for one student.</summary>
    public class StudentAttendanceSummaryRow
    {
        public int StudentId { get; set; }
        public int TotalWorkingDays { get; set; }
        public int TotalPresentDays { get; set; }
        public int TotalAbsentDays { get; set; }
        public int PeriodsRecorded { get; set; }
        public decimal? OverallPercent { get; set; }
    }

    /// <summary>One row in the daily entry grid (one per active student).</summary>
    public class DailyAttendanceGridRow
    {
        public int StudentId { get; set; }
        public string? StudentCode { get; set; }
        public string? FormNo { get; set; }
        public string? FullName { get; set; }
        public string? StudentStatus { get; set; }
        public int? AttendanceId { get; set; }
        public string? DayStatus { get; set; }   // Present / Absent / Late / NULL (not marked)
        public string? Remarks { get; set; }
        public DateTime? MarkedAt { get; set; }
    }

    /// <summary>One day in a student's history.</summary>
    public class StudentDailyAttendanceRow
    {
        public int Id { get; set; }
        public DateTime AttendanceDate { get; set; }
        public string? Status { get; set; }
        public string? Remarks { get; set; }
        public string? ClassName { get; set; }
        public DateTime? MarkedAt { get; set; }
    }

    /// <summary>One row in the class-scoped attendance summary board (multi-student).</summary>
    public class StudentAttendanceBoardRow
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        public string? ClassName { get; set; }
        public int StudentId { get; set; }
        public string? StudentCode { get; set; }
        public string? StudentName { get; set; }
        public int PeriodYear { get; set; }
        public int PeriodMonth { get; set; }
        public int WorkingDays { get; set; }
        public int PresentDays { get; set; }
        public int AbsentDays { get; set; }
        public decimal? AttendancePercent { get; set; }
        public string? Remarks { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
