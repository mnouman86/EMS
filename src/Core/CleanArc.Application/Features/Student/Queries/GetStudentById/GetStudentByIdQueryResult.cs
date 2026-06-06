using System;

namespace CleanArc.Application.Features.Student.Queries.GetStudentById
{
    public class GetStudentByIdQueryResult
    {
        public int Id { get; set; }
        public string StudentCode { get; set; }
        public string FormNo { get; set; }

        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Religion { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? GradeApplyingForId { get; set; }
        public int? AdmittedClassId { get; set; }
        public string AdmittedLevelCode { get; set; }

        public string ParentName { get; set; }
        public string ParentRelationship { get; set; }
        public string ParentEmail { get; set; }
        public string ParentMobile { get; set; }
        public string HomeAddress { get; set; }

        public string EmergencyContactName { get; set; }
        public string EmergencyContactPhone { get; set; }

        public bool? HasMedicalConditions { get; set; }
        public string MedicalDetails { get; set; }
        public string PreviousSchoolName { get; set; }
        public string PreviousSchoolClass { get; set; }
        public DateTime? PreviousSchoolDateLeft { get; set; }

        public bool? DeclarationAccepted { get; set; }
        public DateTime? DeclarationAcceptedAt { get; set; }
        public string SignatureName { get; set; }
        public string SignatureImagePath { get; set; }
        public DateTime? SignatureDate { get; set; }

        public string Status { get; set; }
        public DateTime? AdmittedAt { get; set; }
        public DateTime? LeftOrAlumniAt { get; set; }
        public string LifecycleReason { get; set; }
    }
}
