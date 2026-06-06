using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.Student.Command.UpdateStudentCommand
{
    public record UpdateStudentCommand(
        int Id,
        string? FullName,
        string? Gender,
        string? Religion,
        DateTime? DateOfBirth,
        int GradeApplyingForId,
        string? ParentName,
        string? ParentRelationship,
        string? ParentEmail,
        string? ParentMobile,
        string? HomeAddress,
        string? EmergencyContactName,
        string? EmergencyContactPhone,
        bool HasMedicalConditions,
        string? MedicalDetails,
        string? PreviousSchoolName,
        string? PreviousSchoolClass,
        DateTime? PreviousSchoolDateLeft,
        int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
        IValidatableModel<UpdateStudentCommand>
    {
        [JsonIgnore]
        public int UserId { get; set; }

        public IValidator<UpdateStudentCommand> ValidateApplicationModel(
            ApplicationBaseValidationModelProvider<UpdateStudentCommand> validator)
        {
            validator.RuleFor(c => c.Id).GreaterThan(0).WithMessage("Invalid Id");
            validator.RuleFor(c => c.FullName).NotEmpty().WithMessage("Full Name is required");
            validator.RuleFor(c => c.GradeApplyingForId).GreaterThan(0).WithMessage("Grade is required");
            validator.RuleFor(c => c.ParentMobile).NotEmpty().WithMessage("Parent Mobile is required");
            return validator;
        }
    }
}
