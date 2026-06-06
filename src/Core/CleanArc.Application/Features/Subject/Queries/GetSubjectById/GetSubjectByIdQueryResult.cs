namespace CleanArc.Application.Features.Subject.Queries.GetSubjectById
{
    public class GetSubjectByIdQueryResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortCode { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsRTL { get; set; }
        public bool IsActive { get; set; }
        public int? MappedClassCount { get; set; }
    }
}
