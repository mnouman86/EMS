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

namespace CleanArc.Application.Features.ActivityPricePerParticipant.Commands.CreateActivityPricePerParticipantCommand;
public record CreateActivityPricePerParticipantCommand(int? GenericTitleId, int PerParticipationPrice, int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreateActivityPricePerParticipantCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateActivityPricePerParticipantCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateActivityPricePerParticipantCommand> validator)
    {
        //validator.RuleFor(c => c.Name)
        //    .NotEmpty()
        //    .NotNull()
        //    .WithMessage("Please enter a valid Name");
        //validator.RuleFor(c => c.Description)
        //    .NotEmpty()
        //    .NotNull()
        //    .WithMessage("Please enter a Description");
        return validator;
    }
}

