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
using CleanArc.Application.Models.Request;using System.Text.Json.Serialization; using CleanArc.Domain.Common;

namespace CleanArc.Application.Features.AmenityMapping.Command.CreateAmenityMappingCommand;

public record CreateAmenityMappingCommand(int? GenericTitleId, int[]? AmenitiesIDs, int? CutureId, int? AminityTypeEnumId) : IRequest<OperationResult<ResponseEntity>>,
IValidatableModel<CreateAmenityMappingCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateAmenityMappingCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateAmenityMappingCommand> validator)
    {
        validator.RuleFor(c => c.GenericTitleId)
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
