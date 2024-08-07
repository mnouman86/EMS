using CleanArc.Application.Features.AgeType.Commands.DeleteAgeTypeCommand;
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

namespace CleanArc.Application.FeaturesppingHotelAmenity.Command.DeleteMappingHotelAmenityCommand;

public record DeleteMappingHotelAmenityCommand(string SelectedIds, int? UpdatedBy, int? CultureId) : IRequest<OperationResult<bool>>,
    IValidatableModel<DeleteMappingHotelAmenityCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<DeleteMappingHotelAmenityCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<DeleteMappingHotelAmenityCommand> validator)
    {
        validator.RuleFor(c => c.SelectedIds)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please select Record");

        return validator;
    }
}
