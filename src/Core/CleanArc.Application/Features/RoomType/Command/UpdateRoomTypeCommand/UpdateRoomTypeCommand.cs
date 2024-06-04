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

namespace CleanArc.Application.Features.RoomType.Command.UpdateRoomTypeCommand;

public record UpdateRoomTypeCommand(int ID, String? Name, string? Description, int? UpdatedBy) : IRequest<OperationResult<bool>>,
    IValidatableModel<UpdateRoomTypeCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateRoomTypeCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateRoomTypeCommand> validator)
    {
        validator.RuleFor(c => c.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid Name");
        validator.RuleFor(c => c.Description)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a Description");
        return validator;
    }
}
