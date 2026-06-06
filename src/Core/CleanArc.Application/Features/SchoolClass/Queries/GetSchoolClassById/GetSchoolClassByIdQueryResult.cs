namespace CleanArc.Application.Features.SchoolClass.Queries.GetSchoolClassById
{
    public class GetSchoolClassByIdQueryResult
    {
        public int Id { get; set; }
        public string LevelName { get; set; }
        public string LevelCode { get; set; }
        public int? GradeNumber { get; set; }
        public int DisplayOrder { get; set; }
        public int? Capacity { get; set; }
        public int? ClassTeacherId { get; set; }
        public string ClassTeacherName { get; set; }
        public int? CurrentStudentCount { get; set; }
        public bool IsActive { get; set; }
    }
}
