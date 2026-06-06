using System;

namespace CleanArc.Application.Features.Employee.Queries.GetTeacherAssignments
{
    public class GetTeacherAssignmentsQueryResult
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int SchoolClassId { get; set; }
        public int SubjectId { get; set; }
        public string ClassName { get; set; }
        public string SubjectName { get; set; }
        public DateTime? AssignedAt { get; set; }
    }
}
