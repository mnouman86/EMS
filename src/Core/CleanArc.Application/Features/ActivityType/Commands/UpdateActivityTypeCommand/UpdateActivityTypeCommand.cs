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

namespace CleanArc.Application.Features.ActivityType.Commands.UpdateActivityTypeCommand;
public record UpdateActivityTypeCommand(int ID,String? Name, string? Description, int? UpdatedBy, int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<UpdateActivityTypeCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateActivityTypeCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateActivityTypeCommand> validator)
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

