using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.Subject.Command.MapSubjectsToClassCommand
{
    public record MapSubjectsToClassCommand(
        int SchoolClassId,
        List<int>? SubjectIds,
        int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
        IValidatableModel<MapSubjectsToClassCommand>
    {
        [JsonIgnore]
        public int UserId { get; set; }

        public IValidator<MapSubjectsToClassCommand> ValidateApplicationModel(
            ApplicationBaseValidationModelProvider<MapSubjectsToClassCommand> validator)
        {
            validator.RuleFor(c => c.SchoolClassId)
                .GreaterThan(0)
                .WithMessage("Invalid School Class");
            return validator;
        }
    }
}
