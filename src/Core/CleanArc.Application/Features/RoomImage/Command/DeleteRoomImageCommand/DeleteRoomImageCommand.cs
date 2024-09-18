using CleanArc.Application.Features.SearchHotelImage.Command.DeleteSearchHotelImageCommand;
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
using System.Text.Json.Serialization; using CleanArc.Domain.Common;

namespace CleanArc.Application.Features.RoomImage.Command.DeleteRoomImageCommand;

public record DeleteRoomImageCommand(string SelectedIds, int? UpdatedBy) : IRequest<OperationResult<ResponseEntity>>,
IValidatableModel<DeleteRoomImageCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<DeleteRoomImageCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<DeleteRoomImageCommand> validator)
    {
        validator.RuleFor(c => c.SelectedIds)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please select Record");

        return validator;
    }
}
