using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.Student.Command.DeleteStudentCommand
{
    public record DeleteStudentCommand(DeleteRequest deleteRequest) : IRequest<OperationResult<ResponseEntity>>,
        IValidatableModel<DeleteStudentCommand>
    {
        [JsonIgnore]
        public int UserId { get; set; }

        public IValidator<DeleteStudentCommand> ValidateApplicationModel(
            ApplicationBaseValidationModelProvider<DeleteStudentCommand> validator)
        {
            validator.RuleFor(c => c.deleteRequest.SelectedIds)
                .NotEmpty().NotNull()
                .WithMessage("Please select a Record");
            return validator;
        }
    }
}
