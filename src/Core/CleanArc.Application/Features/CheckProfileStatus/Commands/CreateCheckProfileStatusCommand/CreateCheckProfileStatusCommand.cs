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

namespace CleanArc.Application.Features.CheckProfileStatus.Commands.CreateCheckProfileStatusCommand;
public record CreateCheckProfileStatusCommand(int? UserID, string? UserIntrestIDs,  int? CreatedBy, int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreateCheckProfileStatusCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateCheckProfileStatusCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateCheckProfileStatusCommand> validator)
    {
       
        validator.RuleFor(c => c.UserID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid UserID");
        validator.RuleFor(c => c.UserIntrestIDs)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid UserIntrestIDs");

        return validator;
    }
}

