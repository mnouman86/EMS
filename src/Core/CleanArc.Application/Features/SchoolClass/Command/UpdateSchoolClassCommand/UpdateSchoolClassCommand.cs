using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.SchoolClass.Command.UpdateSchoolClassCommand
{
    public record UpdateSchoolClassCommand(
        int Id,
        string? LevelName,
        int? GradeNumber,
        int DisplayOrder,
        int? Capacity,
        int? ClassTeacherId,
        bool IsActive,
        int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
        IValidatableModel<UpdateSchoolClassCommand>
    {
        [JsonIgnore]
        public int UserId { get; set; }

        public IValidator<UpdateSchoolClassCommand> ValidateApplicationModel(
            ApplicationBaseValidationModelProvider<UpdateSchoolClassCommand> validator)
        {
            validator.RuleFor(c => c.Id).GreaterThan(0).WithMessage("Invalid Id");

            validator.RuleFor(c => c.LevelName)
                .NotEmpty().NotNull()
                .WithMessage("Please enter a valid Level Name");

            validator.RuleFor(c => c.DisplayOrder)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Display Order must be 0 or greater");

            return validator;
        }
    }
}
