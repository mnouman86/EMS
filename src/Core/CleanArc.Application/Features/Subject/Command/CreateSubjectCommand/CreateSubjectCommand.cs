using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.Subject.Command.CreateSubjectCommand
{
    public record CreateSubjectCommand(
        string? Name,
        string? ShortCode,
        int DisplayOrder,
        bool IsRTL,
        bool IsActive,
        int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
        IValidatableModel<CreateSubjectCommand>
    {
        [JsonIgnore]
        public int UserId { get; set; }

        public IValidator<CreateSubjectCommand> ValidateApplicationModel(
            ApplicationBaseValidationModelProvider<CreateSubjectCommand> validator)
        {
            validator.RuleFor(c => c.Name)
                .NotEmpty().NotNull()
                .MaximumLength(100)
                .WithMessage("Please enter a valid Subject Name");

            validator.RuleFor(c => c.ShortCode)
                .MaximumLength(10)
                .When(c => !string.IsNullOrWhiteSpace(c.ShortCode))
                .WithMessage("Short Code must be at most 10 characters");

            validator.RuleFor(c => c.DisplayOrder)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Display Order must be 0 or greater");

            return validator;
        }
    }
}
