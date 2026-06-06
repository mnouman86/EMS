using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.SchoolClass.Command.AssignClassTeacherCommand
{
    public record AssignClassTeacherCommand(
        int SchoolClassId,
        int? ClassTeacherId,
        int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
        IValidatableModel<AssignClassTeacherCommand>
    {
        [JsonIgnore]
        public int UserId { get; set; }

        public IValidator<AssignClassTeacherCommand> ValidateApplicationModel(
            ApplicationBaseValidationModelProvider<AssignClassTeacherCommand> validator)
        {
            validator.RuleFor(c => c.SchoolClassId)
                .GreaterThan(0)
                .WithMessage("Invalid School Class");
            return validator;
        }
    }
}
