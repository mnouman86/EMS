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

namespace CleanArc.Application.Features.ActivityPrivateParticipant.Commands.DeleteActivityPrivateParticipantCommand;

public record DeleteActivityPrivateParticipantCommand(string SelectedIds, int? UpdatedBy) : IRequest<OperationResult<bool>>,
    IValidatableModel<DeleteActivityPrivateParticipantCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<DeleteActivityPrivateParticipantCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<DeleteActivityPrivateParticipantCommand> validator)
    {
        validator.RuleFor(c => c.SelectedIds)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please select Record");
       
        return validator;
    }
}

//public record class DeleteActivityPrivateParticipantCommand
//{
//}
