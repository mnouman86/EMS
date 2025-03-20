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

namespace CleanArc.Application.Features.MappingCarAmenity.Command.UpdateMappingCarAmenityCommand;

public record UpdateMappingCarAmenityCommand(int Id, String? Name, string? Description, int? CultureID) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<UpdateMappingCarAmenityCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateMappingCarAmenityCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateMappingCarAmenityCommand> validator)
    {
        validator.RuleFor(c => c.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid Name");
        return validator;
    }
}

