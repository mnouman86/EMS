using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using CleanArc.SharedKernel.ValidationBase;
using FluentValidation;
using System.Text.Json.Serialization; using CleanArc.Domain.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.CoreArea.Commands.CreateCoreAreaCommand;
public record CreateCoreAreaCommand(string? Name, string? Description, int? Type, int? CreatedBy, int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreateCoreAreaCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateCoreAreaCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateCoreAreaCommand> validator)
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

