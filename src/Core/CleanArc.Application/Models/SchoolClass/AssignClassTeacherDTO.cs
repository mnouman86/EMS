namespace CleanArc.Application.Models.SchoolClass
{
    public class AssignClassTeacherDTO
    {
        public int SchoolClassId { get; set; }
        public int? ClassTeacherId { get; set; }
        public int UpdatedBy { get; set; }
        public int CultureId { get; set; }
    }
}
