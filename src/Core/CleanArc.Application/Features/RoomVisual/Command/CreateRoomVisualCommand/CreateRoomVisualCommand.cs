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

namespace CleanArc.Application.Features.RoomVisual.Command.CreateRoomVisualCommand;

public record CreateRoomVisualCommand(int? HotelID, int? RoomID,int? CategoryID , String? ImagePath, string? ImageTitle, bool? IsMain, int? CreatedBy) : IRequest<OperationResult<bool>>,
    IValidatableModel<CreateRoomVisualCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateRoomVisualCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateRoomVisualCommand> validator)
    {
        validator.RuleFor(c => c.HotelID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid HotelID");
        validator.RuleFor(c => c.RoomID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a RoomID");
        validator.RuleFor(c => c.CategoryID)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a valid CategoryID");
        validator.RuleFor(c => c.ImageTitle)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a ImageTitle");
        validator.RuleFor(c => c.ImagePath)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a ImagePath");
        return validator;
    }
}
