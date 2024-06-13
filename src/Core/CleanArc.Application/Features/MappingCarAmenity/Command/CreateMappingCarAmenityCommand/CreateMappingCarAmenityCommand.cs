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

namespace CleanArc.Application.Features.MappingCarAmenity.Command.CreateMappingCarAmenityCommand;

public record CreateMappingCarAmenityCommand(int CarID, string? AmenitiesIDs, int? CreatedBy, int? UpdatedBy) : IRequest<OperationResult<bool>>,
IValidatableModel<CreateMappingCarAmenityCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateMappingCarAmenityCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateMappingCarAmenityCommand> validator)
    {
        validator.RuleFor(c => c.CarID)
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
