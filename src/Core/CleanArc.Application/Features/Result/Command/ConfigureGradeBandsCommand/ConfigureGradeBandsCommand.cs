using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.Result.Command.ConfigureGradeBandsCommand
{
    public record GradeBandInput(string Grade, decimal MinPercent, decimal MaxPercent, string? RemarkTemplate, int DisplayOrder);

    public record ConfigureGradeBandsCommand(List<GradeBandInput> Bands)
        : IRequest<OperationResult<ResponseEntity>>,
          IValidatableModel<ConfigureGradeBandsCommand>
    {
        [JsonIgnore]
        public int UserId { get; set; }

        public IValidator<ConfigureGradeBandsCommand> ValidateApplicationModel(
            ApplicationBaseValidationModelProvider<ConfigureGradeBandsCommand> validator)
        {
            validator.RuleFor(c => c.Bands).NotNull().NotEmpty().WithMessage("At least one band is required");
            validator.RuleForEach(c => c.Bands).ChildRules(b =>
            {
                b.RuleFor(x => x.Grade).NotEmpty();
                b.RuleFor(x => x.MinPercent).InclusiveBetween(0, 100);
                b.RuleFor(x => x.MaxPercent).InclusiveBetween(0, 100);
                b.RuleFor(x => x).Must(x => x.MinPercent <= x.MaxPercent)
                    .WithMessage("MinPercent must be ≤ MaxPercent");
            });
            return validator;
        }
    }
}
