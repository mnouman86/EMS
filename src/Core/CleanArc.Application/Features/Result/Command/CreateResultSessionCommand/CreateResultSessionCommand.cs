using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.Result.Command.CreateResultSessionCommand
{
    public record CreateResultSessionCommand(
        string? Name,
        int MaxWritten,
        int MaxOral,
        int MaxAttribute,
        decimal WeightWritten,
        decimal WeightOral,
        decimal WeightPerformance,
        int RoundingDecimals) : IRequest<OperationResult<ResponseEntity>>,
        IValidatableModel<CreateResultSessionCommand>
    {
        [JsonIgnore]
        public int UserId { get; set; }

        public IValidator<CreateResultSessionCommand> ValidateApplicationModel(
            ApplicationBaseValidationModelProvider<CreateResultSessionCommand> validator)
        {
            validator.RuleFor(c => c.Name).NotEmpty().WithMessage("Session Name is required");
            validator.RuleFor(c => c.MaxWritten).GreaterThan(0);
            validator.RuleFor(c => c.MaxOral).GreaterThan(0);
            validator.RuleFor(c => c.MaxAttribute).GreaterThan(0);
            validator.RuleFor(c => c).Must(c => c.WeightWritten + c.WeightOral + c.WeightPerformance == 100m)
                .WithMessage("Weightages must sum to 100");
            validator.RuleFor(c => c.RoundingDecimals).InclusiveBetween(0, 4);
            return validator;
        }
    }
}
