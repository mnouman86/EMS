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

namespace CleanArc.Application.Features.KBTiming.Commands.CreateKBTimingCommand;
public record CreateKBTimingCommand(int? GenericTitleID, string? Day, string? TimeFrom, string? TimeTo, bool? IsAlwaysOpen, bool? IsClosed, int? CreatedBy, int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreateKBTimingCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateKBTimingCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateKBTimingCommand> validator)
    {
        validator.RuleFor(c => c.GenericTitleID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid KBDetailID");
        validator.RuleFor(c => c.Day)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid Day ");
        //validator.RuleFor(c => c.TimeFrom)
        //    .NotEmpty()
        //    .NotNull()
        //    .WithMessage("Please enter a TimeFrom ");
        //validator.RuleFor(c => c.TimeTo)
        //    .NotEmpty()
        //    .NotNull()
        //    .WithMessage("Please enter a TimeTo ");
        validator.RuleFor(c => c.IsAlwaysOpen)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a IsAlwaysOpen  ");
        return validator;
    }
}

