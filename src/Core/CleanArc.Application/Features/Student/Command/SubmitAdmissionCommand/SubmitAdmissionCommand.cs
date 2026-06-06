using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.Student.Command.SubmitAdmissionCommand
{
    public record SubmitAdmissionCommand(
        /* Student Info */
        string? FullName,
        string? Gender,
        string? Religion,
        DateTime? DateOfBirth,
        int GradeApplyingForId,
        /* Parent / Guardian */
        string? ParentName,
        string? ParentRelationship,
        string? ParentEmail,
        string? ParentMobile,
        string? HomeAddress,
        /* Emergency */
        string? EmergencyContactName,
        string? EmergencyContactPhone,
        /* Additional */
        bool HasMedicalConditions,
        string? MedicalDetails,
        string? PreviousSchoolName,
        string? PreviousSchoolClass,
        DateTime? PreviousSchoolDateLeft,
        /* Declaration & Signature (STU-02) */
        bool DeclarationAccepted,
        string? SignatureName,
        string? SignatureImagePath,
        DateTime? SignatureDate,
        int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
        IValidatableModel<SubmitAdmissionCommand>
    {
        [JsonIgnore]
        public int UserId { get; set; }

        public IValidator<SubmitAdmissionCommand> ValidateApplicationModel(
            ApplicationBaseValidationModelProvider<SubmitAdmissionCommand> validator)
        {
            // Student Info
            validator.RuleFor(c => c.FullName).NotEmpty().WithMessage("Full Name is required");
            validator.RuleFor(c => c.Gender).NotEmpty()
                .Must(g => g == null || new[] { "Male", "Female", "Other" }.Contains(g))
                .WithMessage("Gender must be Male / Female / Other");
            validator.RuleFor(c => c.Religion).NotEmpty()
                .Must(r => r == null || new[] { "Islam", "Christianity", "Other" }.Contains(r))
                .WithMessage("Religion must be Islam / Christianity / Other");
            validator.RuleFor(c => c.DateOfBirth).NotNull().WithMessage("Date of Birth is required")
                .Must(d => !d.HasValue || d.Value.Date < DateTime.UtcNow.Date).WithMessage("Date of Birth cannot be in the future");
            validator.RuleFor(c => c.GradeApplyingForId).GreaterThan(0).WithMessage("Grade Applying For is required");

            // Parent
            validator.RuleFor(c => c.ParentName).NotEmpty().WithMessage("Parent/Guardian Name is required");
            validator.RuleFor(c => c.ParentRelationship).NotEmpty()
                .Must(r => r == null || new[] { "Mother", "Father", "Legal Guardian" }.Contains(r))
                .WithMessage("Parent Relationship must be Mother / Father / Legal Guardian");
            validator.RuleFor(c => c.ParentEmail).NotEmpty().EmailAddress().WithMessage("Valid Parent Email is required");
            validator.RuleFor(c => c.ParentMobile).NotEmpty().WithMessage("Parent Mobile is required");
            validator.RuleFor(c => c.HomeAddress).NotEmpty().WithMessage("Home Address is required");

            // Emergency
            validator.RuleFor(c => c.EmergencyContactName).NotEmpty().WithMessage("Emergency Contact Name is required");
            validator.RuleFor(c => c.EmergencyContactPhone).NotEmpty().WithMessage("Emergency Contact Phone is required");

            // Declaration (STU-02)
            validator.RuleFor(c => c.DeclarationAccepted).Equal(true)
                .WithMessage("Declaration must be accepted to submit the application");
            validator.RuleFor(c => c.SignatureName)
                .NotEmpty().When(c => string.IsNullOrWhiteSpace(c.SignatureImagePath))
                .WithMessage("A typed name or signature image is required");

            return validator;
        }
    }
}
