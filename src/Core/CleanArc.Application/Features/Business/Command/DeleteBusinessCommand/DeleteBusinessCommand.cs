using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using CleanArc.SharedKernel.ValidationBase;
using FluentValidation;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArc.Application.Models.Request;using System.Text.Json.Serialization; using CleanArc.Domain.Common;

namespace CleanArc.Application.Features.Business.Command.DeleteBusinessCommand
{
    public record  DeleteBusinessCommand(DeleteRequest deleteRequest) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<DeleteBusinessCommand>
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public IValidator<DeleteBusinessCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<DeleteBusinessCommand> validator)
        {
            validator.RuleFor(c => c.deleteRequest.SelectedIds)
                .NotEmpty()
                .NotNull()
                .WithMessage("Please select Record");

            return validator;
        }
    }

    //public record class DeleteAgeTypeCommand
    //{
    //}
}
