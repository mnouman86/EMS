using CleanArc.Application.Features.AgeType.Commands.CreateAgeTypeCommand;
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

namespace CleanArc.Application.Features.MappingHotelAmenity.Command.CreateMappingHotelAmenityCommand;

public record CreateMappingHotelAmenityCommand(int HotelID, string? AmenitiesIDs, int? CreatedBy, int? UpdatedBy) : IRequest<OperationResult<bool>>,
IValidatableModel<CreateMappingHotelAmenityCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateMappingHotelAmenityCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateMappingHotelAmenityCommand> validator)
    {
        validator.RuleFor(c => c.HotelID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid HotelIDs");
        validator.RuleFor(c => c.AmenitiesIDs)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a AmenitiesIDs");
        return validator;
    }
}
