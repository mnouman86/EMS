namespace CleanArc.Domain.Entities.Authorization
{
    public class MyClassRow
    {
        public int SchoolClassId { get; set; }
        public string? LevelName { get; set; }
        public string? LevelCode { get; set; }
        public int DisplayOrder { get; set; }
    }

    public class MySubjectRow
    {
        public int SubjectId { get; set; }
        public string? SubjectName { get; set; }
        public string? ShortCode { get; set; }
        public int DisplayOrder { get; set; }
    }

    public class MyClassSubjectRow
    {
        public int SchoolClassId { get; set; }
        public string? LevelName { get; set; }
        public string? LevelCode { get; set; }
        public int SubjectId { get; set; }
        public string? SubjectName { get; set; }
        public string? ShortCode { get; set; }
    }

    /// <summary>Bundle returned to the teacher dashboard in one round-trip.</summary>
    public class MyTeachingBundle
    {
        public List<MyClassRow> ClassesIHead { get; set; } = new();
        public List<MySubjectRow> MySubjects { get; set; } = new();
        public List<MyClassSubjectRow> ClassSubjectMap { get; set; } = new();
    }
}
