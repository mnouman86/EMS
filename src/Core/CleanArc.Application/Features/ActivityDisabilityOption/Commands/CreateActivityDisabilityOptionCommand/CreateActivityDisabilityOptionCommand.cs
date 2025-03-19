using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using CleanArc.SharedKernel.ValidationBase;
using FluentValidation;
using CleanArc.Application.Models.Request;using System.Text.Json.Serialization; using CleanArc.Domain.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.ActivityDisabilityOption.Commands.CreateActivityDisabilityOptionCommand;
public record CreateActivityDisabilityOptionCommand(string? Name, string? Description,int? CreatedBy, int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreateActivityDisabilityOptionCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateActivityDisabilityOptionCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateActivityDisabilityOptionCommand> validator)
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

