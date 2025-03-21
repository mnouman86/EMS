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

namespace CleanArc.Application.Features.DisabilityOption.Commands.UpdateDisabilityOptionCommand;
public record UpdateDisabilityOptionCommand(int Id,String? Name, string? Description, int? UpdatedBy) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<UpdateDisabilityOptionCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateDisabilityOptionCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateDisabilityOptionCommand> validator)
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

