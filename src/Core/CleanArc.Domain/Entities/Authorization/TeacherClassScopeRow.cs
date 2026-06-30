namespace CleanArc.Domain.Entities.Authorization
{
    /// <summary>
    /// One row per (class, source) the calling teacher is responsible for.
    /// Source is 'CT' when the teacher is the class teacher of that class, or
    /// 'ST' when the teacher merely teaches a subject in that class. A teacher
    /// who is both will appear twice (once per source).
    /// </summary>
    public class TeacherClassScopeRow
    {
        public int SchoolClassId { get; set; }
        public string Source { get; set; } = "ST";
    }
}
