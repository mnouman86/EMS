using System;

namespace CleanArc.Domain.Entities.Student
{
    public class Student
    {
        public int Id { get; set; }
        public string? StudentCode { get; set; }     // TSSS-{LEVEL}-0000  (issued on Admit)
        public string? FormNo { get; set; }          // Sequential application number

        /* Student Info */
        public string? FullName { get; set; }
        public string? Gender { get; set; }
        public string? Religion { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? GradeApplyingForId { get; set; } // FK SchoolClass at submission
        public int? AdmittedClassId { get; set; }    // FK SchoolClass once admitted (may differ)
        public string? AdmittedLevelCode { get; set; } // snapshot at admission for the code prefix

        /* Parent / Guardian */
        public string? ParentName { get; set; }
        public string? ParentRelationship { get; set; }  // Mother / Father / Legal Guardian
        public string? ParentEmail { get; set; }
        public string? ParentMobile { get; set; }
        public string? HomeAddress { get; set; }

        /* Emergency Contact */
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }

        /* Additional */
        public bool? HasMedicalConditions { get; set; }
        public string? MedicalDetails { get; set; }
        public string? PreviousSchoolName { get; set; }
        public string? PreviousSchoolClass { get; set; }
        public DateTime? PreviousSchoolDateLeft { get; set; }

        /* Declaration & Signature */
        public bool? DeclarationAccepted { get; set; }
        public DateTime? DeclarationAcceptedAt { get; set; }
        public string? SignatureName { get; set; }       // typed
        public string? SignatureImagePath { get; set; }  // drawn / uploaded
        public DateTime? SignatureDate { get; set; }

        /* Lifecycle */
        public string? Status { get; set; }              // Applied / Admitted / Active / Left / Alumni / Withdrawn
        public DateTime? AdmittedAt { get; set; }
        public DateTime? LeftOrAlumniAt { get; set; }
        public string? LifecycleReason { get; set; }

        /* Audit / soft-delete */
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CultureId { get; set; }
    }

    public class StudentImportRow
    {
        public int? SrNo { get; set; }
        public string? StudentCode { get; set; }
        public string? FormNo { get; set; }
        public string? FullName { get; set; }
        public string? FatherName { get; set; }
        public string? EmergencyContact { get; set; }
        public string? ClassName { get; set; }
    }
}
