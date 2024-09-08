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

namespace CleanArc.Application.Features.KBTiming.Commands.UpdateKBTimingCommand;
public record UpdateKBTimingCommand(int ID,int? KBDetailID, string? Day, string? TimeFrom, string? TimeTo, bool? IsAlwaysOpen, int? UpdatedBy, int? CultureId) : IRequest<OperationResult<bool>>,
    IValidatableModel<UpdateKBTimingCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateKBTimingCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateKBTimingCommand> validator)
    {
        validator.RuleFor(c => c.KBDetailID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid KBDetailID");
        validator.RuleFor(c => c.Day)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid Day ");
        validator.RuleFor(c => c.TimeFrom)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a TimeFrom ");
        validator.RuleFor(c => c.TimeTo)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a TimeTo ");
        validator.RuleFor(c => c.IsAlwaysOpen)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a IsAlwaysOpen  ");
        return validator;
    }
}

