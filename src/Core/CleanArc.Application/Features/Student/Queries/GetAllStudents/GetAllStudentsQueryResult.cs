using System;

namespace CleanArc.Application.Features.Student.Queries.GetAllStudents
{
    public class GetAllStudentsQueryResult
    {
        public int Id { get; set; }
        public string StudentCode { get; set; }
        public string FormNo { get; set; }
        public string FullName { get; set; }
        public string ParentName { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? AdmittedClassId { get; set; }
        public string AdmittedClassName { get; set; }
        public string Status { get; set; }
        public string EmergencyContactPhone { get; set; }
    }
}
