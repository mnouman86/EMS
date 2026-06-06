using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.SchoolClass.Command.CreateSchoolClassCommand
{
    public record CreateSchoolClassCommand(
        string? LevelName,
        string? LevelCode,
        int? GradeNumber,
        int DisplayOrder,
        int? Capacity,
        int? ClassTeacherId,
        bool IsActive,
        int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
        IValidatableModel<CreateSchoolClassCommand>
    {
        [JsonIgnore]
        public int UserId { get; set; }

        public IValidator<CreateSchoolClassCommand> ValidateApplicationModel(
            ApplicationBaseValidationModelProvider<CreateSchoolClassCommand> validator)
        {
            validator.RuleFor(c => c.LevelName)
                .NotEmpty().NotNull()
                .WithMessage("Please enter a valid Level Name");

            validator.RuleFor(c => c.LevelCode)
                .NotEmpty().NotNull()
                .Length(2, 4)
                .Matches("^[A-Za-z]+$")
                .WithMessage("Level Code must be 2-4 letters");

            validator.RuleFor(c => c.DisplayOrder)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Display Order must be 0 or greater");

            return validator;
        }
    }
}
