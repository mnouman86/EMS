using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.SchoolClass.Command.DeleteSchoolClassCommand
{
    public record DeleteSchoolClassCommand(DeleteRequest deleteRequest) : IRequest<OperationResult<ResponseEntity>>,
        IValidatableModel<DeleteSchoolClassCommand>
    {
        [JsonIgnore]
        public int UserId { get; set; }

        public IValidator<DeleteSchoolClassCommand> ValidateApplicationModel(
            ApplicationBaseValidationModelProvider<DeleteSchoolClassCommand> validator)
        {
            validator.RuleFor(c => c.deleteRequest.SelectedIds)
                .NotEmpty().NotNull()
                .WithMessage("Please select a Record");
            return validator;
        }
    }
}
