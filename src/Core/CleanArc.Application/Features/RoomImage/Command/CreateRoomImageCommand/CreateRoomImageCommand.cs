using CleanArc.Application.Features.SearchHotelImage.Command.CreateSearchHotelImageCommand;
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

namespace CleanArc.Application.Features.RoomImage.Command.CreateRoomImageCommand;

public record CreateRoomImageCommand(string? HotelName, string? ImageTitle, string? ImagePath, int? HotelID) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreateRoomImageCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateRoomImageCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateRoomImageCommand> validator)
    {
        validator.RuleFor(c => c.HotelName)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid Name");
        validator.RuleFor(c => c.ImageTitle)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a Description");
        return validator;
    }
}