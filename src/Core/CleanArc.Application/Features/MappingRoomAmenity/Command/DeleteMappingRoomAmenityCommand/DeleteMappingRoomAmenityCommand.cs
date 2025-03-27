using CleanArc.Application.AmenityMapping.Command.DeleteAmenityMappingCommand;
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

namespace CleanArc.Application.Features.MappingRoomAmenity.Command.DeleteMappingRoomAmenityCommand;

public  record DeleteMappingRoomAmenityCommand(DeleteRequest deleteRequest) : IRequest<OperationResult<ResponseEntity>>,
IValidatableModel<DeleteMappingRoomAmenityCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<DeleteMappingRoomAmenityCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<DeleteMappingRoomAmenityCommand> validator)
    {
        validator.RuleFor(c => c.deleteRequest.SelectedIds)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please select Record");

        return validator;
    }
}
