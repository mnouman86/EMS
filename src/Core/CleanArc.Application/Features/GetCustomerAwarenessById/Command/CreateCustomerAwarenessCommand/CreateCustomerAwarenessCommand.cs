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
using CleanArc.Domain.Common;

namespace CleanArc.Application.Features.CustomerAwareness.Command.CreateCustomerAwarenessCommand;

public record CreateCustomerAwarenessCommand(string? Title,
string? Description,
int? KeyNumber,
string? Link,
string? Status,
int? StatusApprovedBy,
string? ApprovedDate,
int? CultureId, 
int? CreatedBy) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreateCustomerAwarenessCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateCustomerAwarenessCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateCustomerAwarenessCommand> validator)
    {
        validator.RuleFor(c => c.Title)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid Name");
        validator.RuleFor(c => c.Description)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a Description");
        validator.RuleFor(c => c.KeyNumber)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a valid KeyNumber");
        validator.RuleFor(c => c.Status)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a Status");
        return validator;
    }
}
