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

namespace CleanArc.Application.Features.CoreArea.Commands.UpdateCoreAreaCommand;
public record UpdateCoreAreaCommand( int ID,String? Name, string? Description, int? Type, int? UpdatedBy, int? CultureId) : IRequest<OperationResult<bool>>,
    IValidatableModel<UpdateCoreAreaCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateCoreAreaCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateCoreAreaCommand> validator)
    {
        
        validator.RuleFor(c => c.Name)
            .NotEmpty()

            .NotNull()
            .WithMessage("Please enter a valid Title");
        validator.RuleFor(c => c.Description)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a Description");
        validator.RuleFor(c => c.Type)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a Type");
        return validator;
    }
}

