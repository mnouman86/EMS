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

namespace CleanArc.Application.Features.CampaignSchedule.Command.UpdateCampaignScheduleCommand;

public record UpdateCampaignScheduleCommand(int Id, string Title,
string Description,
string? StartDate,
//string EndDate,
int? Duration,
string DiscountType,
decimal? DiscountValue,
//string? RecurrenceType,
int? UpdatedBy) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<UpdateCampaignScheduleCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateCampaignScheduleCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateCampaignScheduleCommand> validator)
    {
        validator.RuleFor(c => c.Title)
    .NotEmpty()
    .NotNull()
    .WithMessage("Please enter a valid Title");
        validator.RuleFor(c => c.Description)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a Description");
        validator.RuleFor(c => c.StartDate)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a valid StartDate");
        validator.RuleFor(c => c.Duration)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a Duration");
        validator.RuleFor(c => c.DiscountType)
         .NotEmpty()
         .NotNull()
         .WithMessage("Please enter a valid DiscountType");
        validator.RuleFor(c => c.DiscountValue)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a DiscountValue");
        return validator;
    }
}
