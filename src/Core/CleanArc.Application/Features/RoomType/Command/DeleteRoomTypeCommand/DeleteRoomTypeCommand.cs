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

namespace CleanArc.Application.Features.RoomType.Command.DeleteRoomTypeCommand;

public record DeleteRoomTypeCommand(string SelectedIds, int? UpdatedBy) : IRequest<OperationResult<bool>>,
    IValidatableModel<DeleteRoomTypeCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<DeleteRoomTypeCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<DeleteRoomTypeCommand> validator)
    {
        validator.RuleFor(c => c.SelectedIds)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please select Record");

        return validator;
    }
}
