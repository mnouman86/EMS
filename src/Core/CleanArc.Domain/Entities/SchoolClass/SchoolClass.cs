using System;

namespace CleanArc.Domain.Entities.SchoolClass
{
    public class SchoolClass
    {
        public int Id { get; set; }
        public string? LevelName { get; set; }
        public string? LevelCode { get; set; }
        public int? GradeNumber { get; set; }
        public int DisplayOrder { get; set; }
        public int? Capacity { get; set; }
        public int? ClassTeacherId { get; set; }
        public string? ClassTeacherName { get; set; }
        public int? CurrentStudentCount { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CultureId { get; set; }
    }
}
