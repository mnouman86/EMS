using CleanArc.Application.Features.AgeType.Commands.UpdateAgeTypeCommand;
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

namespace CleanArc.Application.Features.Amenities.Command.UpdateAmenitiesCommand;

public record UpdateAmenityCommand(int Id, string? Name, string? Description, int? AmenityTypeEnumID, string? Icon, int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<UpdateAmenityCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateAmenityCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateAmenityCommand> validator)
    {
        validator.RuleFor(c => c.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid Name");
        validator.RuleFor(c => c.AmenityTypeEnumID)
          .NotEmpty()
          .NotNull()
          .WithMessage("Please select a AmenityTypeEnumID");
        return validator;
    }
}
