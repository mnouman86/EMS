using System;

namespace CleanArc.Domain.Entities.AcademicYear
{
    public class AcademicYear
    {
        public int Id { get; set; }
        public string? Code { get; set; }          // e.g. "2025-26"
        public string? DisplayName { get; set; }   // e.g. "Academic Year 2025–26"
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsOpen { get; set; }           // exactly one IsOpen=true at a time
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
