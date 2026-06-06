namespace CleanArc.Application.Features.Subject.Queries.GetSubjectsByClass
{
    public class GetSubjectsByClassQueryResult
    {
        public int Id { get; set; }
        public int SchoolClassId { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public string ShortCode { get; set; }
        public bool IsRTL { get; set; }
        public int DisplayOrder { get; set; }
    }
}
