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

namespace CleanArc.Application.Features.ActivityNature.Commands.UpdateActivityNatureCommand;
public record UpdateActivityNatureCommand(int ID,String? Name, string? Description, int? UpdatedBy) : IRequest<OperationResult<bool>>,
    IValidatableModel<UpdateActivityNatureCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateActivityNatureCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateActivityNatureCommand> validator)
    {
        validator.RuleFor(c => c.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid Name");
        validator.RuleFor(c => c.Description)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a Description");
        return validator;
    }
}

