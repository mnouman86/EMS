using CleanArc.Application.Features.AgeType.Commands.DeleteAgeTypeCommand;
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

namespace CleanArc.Application.Features.RoomVisual.Command.DeleteRoomVisualCommand;

public record DeleteRoomVisualCommand(string SelectedIds, int? UpdatedBy, int? CultureId) : IRequest<OperationResult<bool>>,
IValidatableModel<DeleteRoomVisualCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<DeleteRoomVisualCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<DeleteRoomVisualCommand> validator)
    {
        validator.RuleFor(c => c.SelectedIds)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please select Record");

        return validator;
    }
}

//public record class DeleteAgeTypeCommand
//{
//}
