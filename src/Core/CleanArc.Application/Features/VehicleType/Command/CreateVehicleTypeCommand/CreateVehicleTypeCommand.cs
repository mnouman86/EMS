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

namespace CleanArc.Application.Features.VehicleType.Command.CreateVehicleTypeCommand;

public  record CreateVehicleTypeCommand(string? Name, string? Description, int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreateVehicleTypeCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateVehicleTypeCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateVehicleTypeCommand> validator)
    {
        validator.RuleFor(c => c.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid VehicleType Name");
        //validator.RuleFor(c => c.Icon)
        //    .NotEmpty()
        //    .NotNull()
        //    .WithMessage("Please enter a valid Google Icon code from https://fonts.google.com/icons");
        return validator;
    }
}

