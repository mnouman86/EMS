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
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.Amenities.Command.UpdateAmenitiesCommand;

public record UpdateAmenityCommand(int ID, String? Name, string? Description,int? CategoryID, int? UpdatedBy) : IRequest<OperationResult<bool>>,
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
