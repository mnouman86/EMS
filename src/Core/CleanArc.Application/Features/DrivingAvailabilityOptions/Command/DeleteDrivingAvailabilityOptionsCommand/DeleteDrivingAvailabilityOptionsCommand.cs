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
using CleanArc.Domain.Common;
using CleanArc.Application.Models.Request;

namespace CleanArc.Application.Features.DrivingAvailabilityOptions.Command.DeleteDrivingAvailabilityOptionsCommand;

public record DeleteDrivingAvailabilityOptionsCommand(DeleteRequest deleteRequest) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<DeleteDrivingAvailabilityOptionsCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<DeleteDrivingAvailabilityOptionsCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<DeleteDrivingAvailabilityOptionsCommand> validator)
    {
        validator.RuleFor(c => c.deleteRequest.SelectedIds)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid record ID.");

        return validator;
    }
}

