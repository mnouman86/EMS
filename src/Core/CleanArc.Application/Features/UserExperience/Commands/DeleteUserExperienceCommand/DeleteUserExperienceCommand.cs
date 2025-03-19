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
using System.Text.Json.Serialization; 
using CleanArc.Domain.Common;
using CleanArc.Application.Models.Request;

namespace CleanArc.Application.Features.UserExperience.Commands.DeleteUserExperienceCommand;

public record DeleteUserExperienceCommand(DeleteRequest deleteRequest) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<DeleteUserExperienceCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<DeleteUserExperienceCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<DeleteUserExperienceCommand> validator)
    {
        validator.RuleFor(c => c.deleteRequest.SelectedIds)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please select Record");
       
        return validator;
    }
}

//public record class DeleteUserExperienceCommand
//{
//}
