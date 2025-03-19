using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization; using CleanArc.Domain.Common;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.ServiceCategory.Command.CreateServiceCategoryCommand;

public record CreateServiceCategoryCommand(string? Name, string? Description, string? Icon, int? ServiceId) : IRequest<OperationResult<ResponseEntity>>,
IValidatableModel<CreateServiceCategoryCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateServiceCategoryCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateServiceCategoryCommand> validator)
    {
        validator.RuleFor(c => c.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid Name");
        validator.RuleFor(c => c.Description)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter Description");
        validator.RuleFor(c => c.ServiceId)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please select a Service");
        validator.RuleFor(c => c.Icon)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid Google Icon code from https://fonts.google.com/icons");
        return validator;
    }
}
