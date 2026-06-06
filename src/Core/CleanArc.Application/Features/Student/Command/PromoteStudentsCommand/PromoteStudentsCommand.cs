using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.Student.Command.PromoteStudentsCommand
{
    public record PromoteStudentsCommand(
        int SourceClassId,
        int TargetClassId,
        List<int> StudentIds,
        bool MoveToAlumni,
        int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
        IValidatableModel<PromoteStudentsCommand>
    {
        [JsonIgnore]
        public int UserId { get; set; }

        public IValidator<PromoteStudentsCommand> ValidateApplicationModel(
            ApplicationBaseValidationModelProvider<PromoteStudentsCommand> validator)
        {
            validator.RuleFor(c => c.SourceClassId).GreaterThan(0).WithMessage("Source Class is required");
            validator.RuleFor(c => c.TargetClassId).GreaterThan(0)
                .When(c => !c.MoveToAlumni)
                .WithMessage("Target Class is required unless MoveToAlumni is true");
            validator.RuleFor(c => c.StudentIds).NotEmpty().WithMessage("Select students to promote");
            return validator;
        }
    }
}
