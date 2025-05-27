using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CleanArc.Application.Models.Request;using System.Text.Json.Serialization; using CleanArc.Domain.Common;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.RoomView.Command.UpdateRoomViewCommand;

public record UpdateRoomViewCommand(int Id, string? Name, string? Description, int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
IValidatableModel<UpdateRoomViewCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateRoomViewCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateRoomViewCommand> validator)
    {
        validator.RuleFor(c => c.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid Name");
        return validator;
    }
}
