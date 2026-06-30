using System;

namespace CleanArc.Domain.Entities.Calendar
{
    public class SchoolHolidayRow
    {
        public int Id { get; set; }
        public DateTime HolidayDate { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CalendarConfigRow
    {
        public string? WeekendDays { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class NonWorkingDateRow
    {
        public DateTime NonWorkingDate { get; set; }
        public string? Reason { get; set; }
    }
}
