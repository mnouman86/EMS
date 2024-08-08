using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using CleanArc.SharedKernel.ValidationBase;
using FluentValidation;
using System.Text.Json.Serialization;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.ActivityNature.Commands.CreateActivityNatureCommand;
public record CreateActivityNatureCommand(string? Name, string? Description,int? CreatedBy) : IRequest<OperationResult<bool>>,
    IValidatableModel<CreateActivityNatureCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateActivityNatureCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateActivityNatureCommand> validator)
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

