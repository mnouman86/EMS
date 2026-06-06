using System;

namespace CleanArc.Domain.Entities.Subject
{
    public class Subject
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ShortCode { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsRTL { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? MappedClassCount { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CultureId { get; set; }
    }

    public class SchoolClassSubject
    {
        public int Id { get; set; }
        public int SchoolClassId { get; set; }
        public int SubjectId { get; set; }
        public string? SubjectName { get; set; }
        public string? ShortCode { get; set; }
        public bool IsRTL { get; set; }
        public int DisplayOrder { get; set; }
    }
}
