using CleanArc.Application.Features.MappingHotelAmenity.Command.CreateMappingHotelAmenityCommand;
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

namespace CleanArc.Application.Features.MappingRoomAmenity.Command.CreateMappingRoomAmenityCommand;

public record CreateMappingRoomAmenityCommand(int RoomID, string? AmenitiesIDs, int? CategoryID, int? CreatedBy, int? UpdatedBy) : IRequest<OperationResult<ResponseEntity>>,
IValidatableModel<CreateMappingRoomAmenityCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateMappingRoomAmenityCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateMappingRoomAmenityCommand> validator)
    {
        validator.RuleFor(c => c.RoomID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid HotelIDs");
        validator.RuleFor(c => c.AmenitiesIDs)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a AmenitiesIDs");
        validator.RuleFor(c => c.CategoryID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a CategoryID");
        return validator;
    }
}
