using CleanArc.Application.FeaturesppingHotelAmenity.Command.DeleteMappingHotelAmenityCommand;
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

namespace CleanArc.Application.Features.MappingRoomAmenity.Command.DeleteMappingRoomAmenityCommand;

public  record DeleteMappingRoomAmenityCommand(string SelectedIds, int? UpdatedBy, int? CultureId) : IRequest<OperationResult<bool>>,
IValidatableModel<DeleteMappingRoomAmenityCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<DeleteMappingRoomAmenityCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<DeleteMappingRoomAmenityCommand> validator)
    {
        validator.RuleFor(c => c.SelectedIds)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please select Record");

        return validator;
    }
}
