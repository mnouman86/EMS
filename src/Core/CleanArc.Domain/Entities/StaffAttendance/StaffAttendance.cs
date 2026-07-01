using System;

namespace CleanArc.Domain.Entities.StaffAttendance
{
    public class StaffAttendanceRow
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime AttendanceDate { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public string? Remarks { get; set; }
        public bool IsBackdated { get; set; }
    }

    public class StaffAttendanceOverviewRow : StaffAttendanceRow
    {
        public string? UserEmail { get; set; }
        public string? StaffName { get; set; }
        public string? RoleName { get; set; }
    }
}
