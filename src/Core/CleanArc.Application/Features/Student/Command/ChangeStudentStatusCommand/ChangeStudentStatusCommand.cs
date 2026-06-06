using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.Student.Command.ChangeStudentStatusCommand
{
    public record ChangeStudentStatusCommand(
        int Id,
        string? TargetStatus,        // Admitted / Active / Left / Alumni / Withdrawn
        int? AdmittedClassId,        // required when TargetStatus = Admitted (defaults to GradeApplyingFor if null)
        string? LifecycleReason,
        int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
        IValidatableModel<ChangeStudentStatusCommand>
    {
        [JsonIgnore]
        public int UserId { get; set; }

        public IValidator<ChangeStudentStatusCommand> ValidateApplicationModel(
            ApplicationBaseValidationModelProvider<ChangeStudentStatusCommand> validator)
        {
            validator.RuleFor(c => c.Id).GreaterThan(0).WithMessage("Invalid Student Id");
            validator.RuleFor(c => c.TargetStatus).NotEmpty()
                .Must(s => s == null || new[] { "Admitted", "Active", "Left", "Alumni", "Withdrawn" }.Contains(s))
                .WithMessage("Target Status must be Admitted / Active / Left / Alumni / Withdrawn");
            return validator;
        }
    }
}
