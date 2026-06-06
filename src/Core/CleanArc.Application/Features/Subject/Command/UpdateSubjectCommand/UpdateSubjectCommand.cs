using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.Subject.Command.UpdateSubjectCommand
{
    public record UpdateSubjectCommand(
        int Id,
        string? Name,
        string? ShortCode,
        int DisplayOrder,
        bool IsRTL,
        bool IsActive,
        int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
        IValidatableModel<UpdateSubjectCommand>
    {
        [JsonIgnore]
        public int UserId { get; set; }

        public IValidator<UpdateSubjectCommand> ValidateApplicationModel(
            ApplicationBaseValidationModelProvider<UpdateSubjectCommand> validator)
        {
            validator.RuleFor(c => c.Id).GreaterThan(0).WithMessage("Invalid Id");

            validator.RuleFor(c => c.Name)
                .NotEmpty().NotNull()
                .MaximumLength(100)
                .WithMessage("Please enter a valid Subject Name");

            validator.RuleFor(c => c.DisplayOrder)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Display Order must be 0 or greater");

            return validator;
        }
    }
}
