using System;

namespace CleanArc.Domain.Entities.Employee
{
    public class Employee
    {
        public int Id { get; set; }
        public string? EmployeeCode { get; set; }   // TSSS-EMP-0001 (EMP-02)

        /* ----- Personal (mandatory*) ----- */
        public string? FullName { get; set; }
        public string? FatherOrHusbandName { get; set; }
        public string? Relation { get; set; }           // Father / Husband
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }             // Male / Female / Other
        public string? MaritalStatus { get; set; }
        public string? Nationality { get; set; }
        public string? Religion { get; set; }
        public string? CNIC { get; set; }
        public DateTime? CNICExpiry { get; set; }
        public string? BloodGroup { get; set; }
        public string? MedicalCondition { get; set; }

        /* ----- Employment ----- */
        public string? Designation { get; set; }
        public string? Department { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public string? EmploymentType { get; set; }     // Full-Time / Part-Time / Visiting
        public string? Status { get; set; }             // Active / OnLeave / Left
        public DateTime? LastWorkingDay { get; set; }
        public string? ReasonForLeaving { get; set; }

        /* ----- Contact ----- */
        public string? PersonalMobile { get; set; }
        public string? Whatsapp { get; set; }
        public string? Landline { get; set; }
        public string? AlternateMobile { get; set; }
        public string? OfficialEmail { get; set; }
        public string? PersonalEmail { get; set; }

        /* ----- Address (Present + Permanent) ----- */
        public string? PresentAddress { get; set; }
        public string? PermanentAddress { get; set; }
        public bool? PermanentSameAsPresent { get; set; }

        /* ----- Emergency Contacts ----- */
        public string? EmergencyContact1Name { get; set; }
        public string? EmergencyContact1Relation { get; set; }
        public string? EmergencyContact1Mobile { get; set; }
        public string? EmergencyContact2Name { get; set; }
        public string? EmergencyContact2Relation { get; set; }
        public string? EmergencyContact2Mobile { get; set; }

        /* ----- Qualification (highest) ----- */
        public string? HighestDegree { get; set; }
        public string? FieldOrMajor { get; set; }
        public int? PassingYear { get; set; }
        public string? Institution { get; set; }
        public string? Certifications { get; set; }

        /* ----- Experience (summary) ----- */
        public decimal? TotalExperienceYears { get; set; }
        public string? PreviousOrganisation { get; set; }
        public string? PreviousPosition { get; set; }
        public string? PreviousDuration { get; set; }
        public string? PreviousSubjectsTaught { get; set; }
        public string? PreviousReasonForLeaving { get; set; }

        /* ----- Bank ----- */
        public string? BankName { get; set; }
        public string? BankBranch { get; set; }
        public string? AccountTitle { get; set; }
        public string? AccountNumber { get; set; }

        /* ----- References ----- */
        public string? Reference1Name { get; set; }
        public string? Reference1Designation { get; set; }
        public string? Reference1Organisation { get; set; }
        public string? Reference1Contact { get; set; }
        public string? Reference2Name { get; set; }
        public string? Reference2Designation { get; set; }
        public string? Reference2Organisation { get; set; }
        public string? Reference2Contact { get; set; }

        /* ----- Photo (denormalised pointer for quick render) ----- */
        public string? PhotoPath { get; set; }

        /* ----- Audit / soft-delete ----- */
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CultureId { get; set; }
    }

    public class EmployeeDocument
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string? DocumentType { get; set; }  // Photo / CNIC / Degree / Certificate / Other
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        public string? ContentType { get; set; }
        public long? FileSizeBytes { get; set; }
        public int? UploadedBy { get; set; }
        public DateTime? UploadedAt { get; set; }
    }

    public class TeacherAssignment
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int SchoolClassId { get; set; }
        public int SubjectId { get; set; }
        public string? ClassName { get; set; }
        public string? SubjectName { get; set; }
        public DateTime? AssignedAt { get; set; }
    }

    /* ---------- Foundational extensions for Modules 6–9 (Fee/Payroll/Finance) ---------- */

    public class EmployeeSalary
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal Allowances { get; set; }
        public decimal FixedDeductions { get; set; }     // e.g. PF, statutory
        public string? AllowanceBreakdownJson { get; set; }  // optional per-allowance detail
        public DateTime EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }       // null = current
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public class EmployeeAdvance
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public decimal Amount { get; set; }              // amount issued
        public decimal OutstandingBalance { get; set; }  // amount still to recover
        public string? Reason { get; set; }
        public DateTime IssuedAt { get; set; }
        public string? Status { get; set; }              // Open / Settled / WrittenOff
        public int? IssuedBy { get; set; }
        public DateTime? SettledAt { get; set; }
    }
}
