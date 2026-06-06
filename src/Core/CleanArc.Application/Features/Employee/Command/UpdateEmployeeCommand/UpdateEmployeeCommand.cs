using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.Employee.Command.UpdateEmployeeCommand
{
    public record UpdateEmployeeCommand(
        int Id,
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
        string? Designation,
        string? Department,
        DateTime? DateOfJoining,
        string? EmploymentType,
        string? PersonalMobile,
        string? Whatsapp,
        string? Landline,
        string? AlternateMobile,
        string? OfficialEmail,
        string? PersonalEmail,
        string? PresentAddress,
        string? PermanentAddress,
        bool PermanentSameAsPresent,
        string? EmergencyContact1Name,
        string? EmergencyContact1Relation,
        string? EmergencyContact1Mobile,
        string? EmergencyContact2Name,
        string? EmergencyContact2Relation,
        string? EmergencyContact2Mobile,
        string? HighestDegree,
        string? FieldOrMajor,
        int? PassingYear,
        string? Institution,
        string? Certifications,
        decimal? TotalExperienceYears,
        string? PreviousOrganisation,
        string? PreviousPosition,
        string? PreviousDuration,
        string? PreviousSubjectsTaught,
        string? PreviousReasonForLeaving,
        string? BankName,
        string? BankBranch,
        string? AccountTitle,
        string? AccountNumber,
        string? Reference1Name,
        string? Reference1Designation,
        string? Reference1Organisation,
        string? Reference1Contact,
        string? Reference2Name,
        string? Reference2Designation,
        string? Reference2Organisation,
        string? Reference2Contact,
        int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
        IValidatableModel<UpdateEmployeeCommand>
    {
        [JsonIgnore]
        public int UserId { get; set; }

        public IValidator<UpdateEmployeeCommand> ValidateApplicationModel(
            ApplicationBaseValidationModelProvider<UpdateEmployeeCommand> validator)
        {
            validator.RuleFor(c => c.Id).GreaterThan(0).WithMessage("Invalid Id");
            validator.RuleFor(c => c.FullName).NotEmpty().WithMessage("Full Name is required");
            validator.RuleFor(c => c.CNIC).NotEmpty().Matches(@"^\d{5}-\d{7}-\d$").WithMessage("CNIC must be in 00000-0000000-0 format");
            validator.RuleFor(c => c.PersonalMobile).NotEmpty().Matches(@"^\+92\d{10}$")
                .WithMessage("Personal Mobile must be in +92XXXXXXXXXX format");
            return validator;
        }
    }
}
