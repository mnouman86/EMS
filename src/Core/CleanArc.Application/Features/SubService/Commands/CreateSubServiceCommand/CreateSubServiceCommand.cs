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

namespace CleanArc.Application.Features.SubService.Commands.CreateSubServiceCommand;
public record CreateSubServiceCommand(string? Name, int? ServiceID, string? Description,int? CreatedBy) : IRequest<OperationResult<bool>>,
    IValidatableModel<CreateSubServiceCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateSubServiceCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateSubServiceCommand> validator)
    {
        validator.RuleFor(c => c.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid Name");
        validator.RuleFor(c => c.Description)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a Description");
        validator.RuleFor(c => c.ServiceID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a ServiceID");
        return validator;
    }
}

