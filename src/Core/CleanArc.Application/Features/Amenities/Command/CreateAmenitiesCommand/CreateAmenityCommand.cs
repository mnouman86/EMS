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

namespace CleanArc.Application.Features.Amenities.Command.CreateAmenitiesCommand;

public record CreateAmenityCommand(string? Name, string? Description, int? CategoryID, int? CreatedBy) : IRequest<OperationResult<bool>>,
    IValidatableModel<CreateAmenityCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateAmenityCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateAmenityCommand> validator)
    {
        validator.RuleFor(c => c.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid Name");
        validator.RuleFor(c => c.Description)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a Description");
        validator.RuleFor(c => c.CategoryID)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a CategoryID");
        return validator;
    }
}
