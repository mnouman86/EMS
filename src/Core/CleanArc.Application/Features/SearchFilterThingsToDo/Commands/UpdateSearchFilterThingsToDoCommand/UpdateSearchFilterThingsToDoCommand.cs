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

namespace CleanArc.Application.Features.SearchFilterThingsToDo.Commands.UpdateSearchFilterThingsToDoCommand;
public record UpdateSearchFilterThingsToDoCommand(
   int Id,
    int? CultureId,
    string? Name,
   int[]? LanguageLookUpId,
   int? ServiceLookUpId,
   int? SubServiceLookUpId,
   string? OtherSubService,
   int? MinAge,
   int? MaxAge,
   int? ActivityTypeLookUpId,
   int? ActivityNatureLookUpId,
   int? MaxGroupSize,
   //bool? IsPrivateActivity,
   string? WhoCanParticipate,
   string? WhoCannotParticipate,
   int? ManageActivityLookUpId,
   string? OtherManageActivity,
   int? Days,
   int? Hours,
    string? Description,
    bool? IsTransportation,
    int? TransportationLookUpId,
    bool? IsDisability,
    string? AllowedItems,
    string? NotAllowedItems,
    int? CurrencyLookUpId,
     //Decimal? PerPersonPrice,
     int[]? SeasonLookUpId,
    int[]? IncludeOptionLookUpId,
    int[]? DisabilityOptionLookUpId,
    DateTime? EndDate,
    DateTime? StartDate,
	string? EndTime,
	string? StartTime,
     int? UpdatedBy) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<UpdateSearchFilterThingsToDoCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateSearchFilterThingsToDoCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateSearchFilterThingsToDoCommand> validator)
    {
        validator.RuleFor(c => c.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid Name");
        return validator;
    }
}

