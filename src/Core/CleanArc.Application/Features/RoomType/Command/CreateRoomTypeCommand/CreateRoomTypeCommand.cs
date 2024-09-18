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

namespace CleanArc.Application.Features.RoomType.Command.CreateRoomTypeCommand;

public record CreateRoomTypeCommand(string? Name, string? Description, int? CreatedBy) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreateRoomTypeCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateRoomTypeCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateRoomTypeCommand> validator)
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
