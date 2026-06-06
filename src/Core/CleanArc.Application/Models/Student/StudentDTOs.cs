using System;
using System.Collections.Generic;

namespace CleanArc.Application.Models.Student
{
    public class SubmitAdmissionDTO
    {
        // Student Info
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Religion { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int GradeApplyingForId { get; set; }

        // Parent / Guardian
        public string ParentName { get; set; }
        public string ParentRelationship { get; set; }
        public string ParentEmail { get; set; }
        public string ParentMobile { get; set; }
        public string HomeAddress { get; set; }

        // Emergency
        public string EmergencyContactName { get; set; }
        public string EmergencyContactPhone { get; set; }

        // Additional
        public bool HasMedicalConditions { get; set; }
        public string MedicalDetails { get; set; }
        public string PreviousSchoolName { get; set; }
        public string PreviousSchoolClass { get; set; }
        public DateTime? PreviousSchoolDateLeft { get; set; }

        // Declaration + Signature
        public bool DeclarationAccepted { get; set; }
        public string SignatureName { get; set; }
        public string SignatureImagePath { get; set; }
        public DateTime? SignatureDate { get; set; }

        public int CreatedBy { get; set; }
        public int CultureId { get; set; }
    }

    public class UpdateStudentDTO : SubmitAdmissionDTO
    {
        public int Id { get; set; }
        public int UpdatedBy { get; set; }
        public new int CultureId { get; set; }
    }

    public class ChangeStudentStatusDTO
    {
        public int Id { get; set; }
        public string TargetStatus { get; set; }      // Admitted / Active / Left / Alumni / Withdrawn
        public int? AdmittedClassId { get; set; }      // used when TargetStatus = Admitted
        public string LifecycleReason { get; set; }
        public int UpdatedBy { get; set; }
        public int CultureId { get; set; }
    }

    public class BulkImportStudentsDTO
    {
        public string RowsJson { get; set; }          // serialized JSON of import rows
        public bool UpdateExisting { get; set; }      // configurable: skip vs update
        public int CreatedBy { get; set; }
        public int CultureId { get; set; }
    }

    public class PromoteStudentsDTO
    {
        public int SourceClassId { get; set; }
        public int TargetClassId { get; set; }
        public string StudentIds { get; set; }        // CSV
        public bool MoveToAlumni { get; set; }        // for Mastery & Transition graduates
        public int UpdatedBy { get; set; }
        public int CultureId { get; set; }
    }
}
