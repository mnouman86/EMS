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
using System.Text.Json.Serialization; using CleanArc.Domain.Common;

namespace CleanArc.Application.Features.SubService.Commands.UpdateSubServiceCommand;
public record UpdateSubServiceCommand(int ID,String? Name, int? ServiceID, string? Description, int? UpdatedBy) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<UpdateSubServiceCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateSubServiceCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateSubServiceCommand> validator)
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

