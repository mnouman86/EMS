namespace CleanArc.Application.Models.Subject
{
    public class CreateSubjectDTO
    {
        public string Name { get; set; }
        public string ShortCode { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsRTL { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public int CultureId { get; set; }
    }
}
