namespace CleanArc.Application.Models.SchoolClass
{
    public class CreateSchoolClassDTO
    {
        public string LevelName { get; set; }
        public string LevelCode { get; set; }
        public int? GradeNumber { get; set; }
        public int DisplayOrder { get; set; }
        public int? Capacity { get; set; }
        public int? ClassTeacherId { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public int CultureId { get; set; }
    }
}
