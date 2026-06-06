using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.Result.Command.LockClassResultsCommand
{
    public record LockClassResultsCommand(int ResultSessionId, int SchoolClassId, string? Reason)
        : IRequest<OperationResult<ResponseEntity>>,
          IValidatableModel<LockClassResultsCommand>
    {
        [JsonIgnore]
        public int UserId { get; set; }

        public IValidator<LockClassResultsCommand> ValidateApplicationModel(
            ApplicationBaseValidationModelProvider<LockClassResultsCommand> validator)
        {
            validator.RuleFor(c => c.ResultSessionId).GreaterThan(0);
            validator.RuleFor(c => c.SchoolClassId).GreaterThan(0);
            return validator;
        }
    }
}
