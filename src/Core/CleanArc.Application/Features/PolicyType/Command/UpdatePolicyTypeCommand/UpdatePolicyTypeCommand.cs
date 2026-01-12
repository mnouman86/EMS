using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CleanArc.Application.Models.Request;using System.Text.Json.Serialization; using CleanArc.Domain.Common;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.PolicyType.Command.UpdatePolicyTypeCommand;

public record UpdatePolicyTypeCommand(int Id, string? Name, string? Description, int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
IValidatableModel<UpdatePolicyTypeCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdatePolicyTypeCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdatePolicyTypeCommand> validator)
    {
        validator.RuleFor(c => c.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid type");
        return validator;
    }
}
