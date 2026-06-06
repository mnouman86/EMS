using System;

namespace CleanArc.Application.Models.Employee
{
    public class UpdateEmployeeDTO
    {
        public int Id { get; set; }

        public string FullName { get; set; }
        public string FatherOrHusbandName { get; set; }
        public string Relation { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string Nationality { get; set; }
        public string Religion { get; set; }
        public string CNIC { get; set; }
        public DateTime? CNICExpiry { get; set; }
        public string BloodGroup { get; set; }
        public string MedicalCondition { get; set; }

        public string Designation { get; set; }
        public string Department { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public string EmploymentType { get; set; }

        public string PersonalMobile { get; set; }
        public string Whatsapp { get; set; }
        public string Landline { get; set; }
        public string AlternateMobile { get; set; }
        public string OfficialEmail { get; set; }
        public string PersonalEmail { get; set; }

        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public bool PermanentSameAsPresent { get; set; }

        public string EmergencyContact1Name { get; set; }
        public string EmergencyContact1Relation { get; set; }
        public string EmergencyContact1Mobile { get; set; }
        public string EmergencyContact2Name { get; set; }
        public string EmergencyContact2Relation { get; set; }
        public string EmergencyContact2Mobile { get; set; }

        public string HighestDegree { get; set; }
        public string FieldOrMajor { get; set; }
        public int? PassingYear { get; set; }
        public string Institution { get; set; }
        public string Certifications { get; set; }

        public decimal? TotalExperienceYears { get; set; }
        public string PreviousOrganisation { get; set; }
        public string PreviousPosition { get; set; }
        public string PreviousDuration { get; set; }
        public string PreviousSubjectsTaught { get; set; }
        public string PreviousReasonForLeaving { get; set; }

        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string AccountTitle { get; set; }
        public string AccountNumber { get; set; }

        public string Reference1Name { get; set; }
        public string Reference1Designation { get; set; }
        public string Reference1Organisation { get; set; }
        public string Reference1Contact { get; set; }
        public string Reference2Name { get; set; }
        public string Reference2Designation { get; set; }
        public string Reference2Organisation { get; set; }
        public string Reference2Contact { get; set; }

        public int UpdatedBy { get; set; }
        public int CultureId { get; set; }
    }

    public class MarkEmployeeLeftDTO
    {
        public int Id { get; set; }
        public DateTime? LastWorkingDay { get; set; }
        public string ReasonForLeaving { get; set; }
        public int UpdatedBy { get; set; }
        public int CultureId { get; set; }
    }

    public class AssignTeacherSubjectsDTO
    {
        public int EmployeeId { get; set; }
        public string ClassSubjectPairs { get; set; }  // CSV of "classId:subjectId" e.g. "4:2,4:3,5:2"
        public int UpdatedBy { get; set; }
        public int CultureId { get; set; }
    }

    public class UploadEmployeeDocumentDTO
    {
        public int EmployeeId { get; set; }
        public string DocumentType { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string ContentType { get; set; }
        public long FileSizeBytes { get; set; }
        public bool IsPhoto { get; set; }   // when true also stamps Employee.PhotoPath
        public int UploadedBy { get; set; }
        public int CultureId { get; set; }
    }

    /* ----- Foundational extensions for Modules 6–9 (Payroll / Finance) ----- */

    public class UpsertEmployeeSalaryDTO
    {
        public int EmployeeId { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal Allowances { get; set; }
        public decimal FixedDeductions { get; set; }
        public string AllowanceBreakdownJson { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public int CreatedBy { get; set; }
    }

    public class IssueEmployeeAdvanceDTO
    {
        public int EmployeeId { get; set; }
        public decimal Amount { get; set; }
        public string Reason { get; set; }
        public int IssuedBy { get; set; }
    }

    public class AdjustEmployeeAdvanceDTO
    {
        public int AdvanceId { get; set; }
        public decimal AmountAdjusted { get; set; }   // positive = repayment, negative = increase
        public string Reason { get; set; }
        public int UpdatedBy { get; set; }
    }
}
