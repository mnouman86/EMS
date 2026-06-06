using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.Result.Command.UnlockClassResultsCommand
{
    public record UnlockClassResultsCommand(int ResultSessionId, int SchoolClassId, string? Reason)
        : IRequest<OperationResult<ResponseEntity>>,
          IValidatableModel<UnlockClassResultsCommand>
    {
        [JsonIgnore]
        public int UserId { get; set; }

        public IValidator<UnlockClassResultsCommand> ValidateApplicationModel(
            ApplicationBaseValidationModelProvider<UnlockClassResultsCommand> validator)
        {
            validator.RuleFor(c => c.ResultSessionId).GreaterThan(0);
            validator.RuleFor(c => c.SchoolClassId).GreaterThan(0);
            validator.RuleFor(c => c.Reason).NotEmpty().WithMessage("Reason is required for unlock (audit)");
            return validator;
        }
    }
}
