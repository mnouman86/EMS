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

namespace CleanArc.Application.Features.ActivityPricePerParticipant.Commands.UpdateActivityPricePerParticipantCommand;
public record UpdateActivityPricePerParticipantCommand( int? Id, Decimal? PerPersonPrice,int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<UpdateActivityPricePerParticipantCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateActivityPricePerParticipantCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateActivityPricePerParticipantCommand> validator)
    {
        validator.RuleFor(c => c.Id)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid ActivityID");
        validator.RuleFor(c => c.PerPersonPrice)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a PerPersonPrice");
        return validator;
    }
}

