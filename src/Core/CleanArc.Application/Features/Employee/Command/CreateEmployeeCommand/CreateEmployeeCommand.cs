using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.Employee.Command.CreateEmployeeCommand
{
    public record CreateEmployeeCommand(
        /* Personal* */
        string? FullName,
        string? FatherOrHusbandName,
        string? Relation,
        DateTime? DateOfBirth,
        string? Gender,
        string? MaritalStatus,
        string? Nationality,
        string? Religion,
        string? CNIC,
        DateTime? CNICExpiry,
        string? BloodGroup,
        string? MedicalCondition,
        /* Employment* */
        string? Designation,
        string? Department,
        DateTime? DateOfJoining,
        string? EmploymentType,
        /* Contact (PersonalMobile*) */
        string? PersonalMobile,
        string? Whatsapp,
        string? Landline,
        string? AlternateMobile,
        string? OfficialEmail,
        string? PersonalEmail,
        /* Address (PresentAddress*) */
        string? PresentAddress,
        string? PermanentAddress,
        bool PermanentSameAsPresent,
        /* Emergency contact 1* */
        string? EmergencyContact1Name,
        string? EmergencyContact1Relation,
        string? EmergencyContact1Mobile,
        string? EmergencyContact2Name,
        string? EmergencyContact2Relation,
        string? EmergencyContact2Mobile,
        /* Qualifications */
        string? HighestDegree,
        string? FieldOrMajor,
        int? PassingYear,
        string? Institution,
        string? Certifications,
        /* Experience */
        decimal? TotalExperienceYears,
        string? PreviousOrganisation,
        string? PreviousPosition,
        string? PreviousDuration,
        string? PreviousSubjectsTaught,
        string? PreviousReasonForLeaving,
        /* Bank */
        string? BankName,
        string? BankBranch,
        string? AccountTitle,
        string? AccountNumber,
        /* References */
        string? Reference1Name,
        string? Reference1Designation,
        string? Reference1Organisation,
        string? Reference1Contact,
        string? Reference2Name,
        string? Reference2Designation,
        string? Reference2Organisation,
        string? Reference2Contact,
        int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
        IValidatableModel<CreateEmployeeCommand>
    {
        [JsonIgnore]
        public int UserId { get; set; }

        public IValidator<CreateEmployeeCommand> ValidateApplicationModel(
            ApplicationBaseValidationModelProvider<CreateEmployeeCommand> validator)
        {
            // Personal
            validator.RuleFor(c => c.FullName).NotEmpty().WithMessage("Full Name is required");
            validator.RuleFor(c => c.FatherOrHusbandName).NotEmpty().WithMessage("Father's/Husband's Name is required");
            validator.RuleFor(c => c.Relation).NotEmpty().WithMessage("Relation is required");
            validator.RuleFor(c => c.DateOfBirth).NotNull().WithMessage("Date of Birth is required")
                .Must(d => !d.HasValue || d.Value.Date < DateTime.UtcNow.Date).WithMessage("Date of Birth must be in the past");
            validator.RuleFor(c => c.Gender).NotEmpty().WithMessage("Gender is required");
            validator.RuleFor(c => c.MaritalStatus).NotEmpty().WithMessage("Marital Status is required");
            validator.RuleFor(c => c.CNIC).NotEmpty().Matches(@"^\d{5}-\d{7}-\d$").WithMessage("CNIC must be in 00000-0000000-0 format");

            // Employment
            validator.RuleFor(c => c.Designation).NotEmpty().WithMessage("Designation is required");
            validator.RuleFor(c => c.DateOfJoining).NotNull().WithMessage("Date of Joining is required");
            validator.RuleFor(c => c.EmploymentType).NotEmpty()
                .Must(t => t == null || new[] { "Full-Time", "Part-Time", "Visiting" }.Contains(t))
                .WithMessage("Employment Type must be Full-Time / Part-Time / Visiting");

            // Contact + Address
            validator.RuleFor(c => c.PersonalMobile).NotEmpty().Matches(@"^\+92\d{10}$")
                .WithMessage("Personal Mobile must be in +92XXXXXXXXXX format");
            validator.RuleFor(c => c.PresentAddress).NotEmpty().WithMessage("Present Address is required");

            // Emergency contact 1
            validator.RuleFor(c => c.EmergencyContact1Name).NotEmpty().WithMessage("Emergency Contact 1 Name is required");
            validator.RuleFor(c => c.EmergencyContact1Relation).NotEmpty().WithMessage("Emergency Contact 1 Relationship is required");
            validator.RuleFor(c => c.EmergencyContact1Mobile).NotEmpty().WithMessage("Emergency Contact 1 Mobile is required");

            return validator;
        }
    }
}
