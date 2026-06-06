using System;

namespace CleanArc.Application.Features.Employee.Queries.GetAllEmployees
{
    public class GetAllEmployeesQueryResult
    {
        public int Id { get; set; }
        public string EmployeeCode { get; set; }
        public string FullName { get; set; }
        public string FatherOrHusbandName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string EmploymentType { get; set; }
        public string Status { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public string CNIC { get; set; }
        public string PersonalMobile { get; set; }
        public string OfficialEmail { get; set; }
        public string PhotoPath { get; set; }
    }
}
