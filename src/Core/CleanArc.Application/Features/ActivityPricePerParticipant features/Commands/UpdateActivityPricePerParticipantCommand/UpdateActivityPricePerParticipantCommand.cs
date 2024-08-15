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

namespace CleanArc.Application.Features.ActivityPricePerParticipant.Commands.UpdateActivityPricePerParticipantCommand;
public record UpdateActivityPricePerParticipantCommand( int? ActivityID, Decimal? PerParticipationPrice, int? UpdatedBy,int? CultureId) : IRequest<OperationResult<bool>>,
    IValidatableModel<UpdateActivityPricePerParticipantCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateActivityPricePerParticipantCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateActivityPricePerParticipantCommand> validator)
    {
        validator.RuleFor(c => c.ActivityID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid ActivityID");
        validator.RuleFor(c => c.PerParticipationPrice)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a PerParticipationPrice");
        return validator;
    }
}

