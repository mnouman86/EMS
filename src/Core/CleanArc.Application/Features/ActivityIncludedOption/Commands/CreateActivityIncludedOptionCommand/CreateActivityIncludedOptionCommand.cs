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

namespace CleanArc.Application.Features.ActivityIncludedOption.Commands.CreateActivityIncludedOptionCommand;
public record CreateActivityIncludedOptionCommand(string? Name, string? Description,int? CreatedBy) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreateActivityIncludedOptionCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateActivityIncludedOptionCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateActivityIncludedOptionCommand> validator)
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

