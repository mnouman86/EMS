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

namespace CleanArc.Application.Features.KBDetail.Commands.UpdateKBDetailCommand;
public record UpdateKBDetailCommand(int ID,
    string? Title,
    string? KeyDate,
    string? Cost,
    int? ServiceID,
    int? CoreAreaLookupID,
    int? RelatedUrlLinkLookupID,
    string? RelatedAreasLookupIDs,
    string? Access,
    string? Availablity,
    string? WhenToVisitIDs,
    //string? Description,
    int? UpdatedBy, 
    int? CultureId) : IRequest<OperationResult<bool>>,
    IValidatableModel<UpdateKBDetailCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateKBDetailCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateKBDetailCommand> validator)
    {
        validator.RuleFor(c => c.Title)
     .NotEmpty()
     .NotNull()
     .WithMessage("Please enter a valid Title");
        validator.RuleFor(c => c.KeyDate)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a KeyDate");
        validator.RuleFor(c => c.Cost)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a Cost");
        validator.RuleFor(c => c.ServiceID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a ServiceID");
        validator.RuleFor(c => c.CoreAreaLookupID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a CoreAreaLookupID");
        validator.RuleFor(c => c.RelatedUrlLinkLookupID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a RelatedUrlLinkLookupID");
        validator.RuleFor(c => c.Access)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a Access");
        validator.RuleFor(c => c.Availablity)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a Availablity");

        validator.RuleFor(c => c.WhenToVisitIDs)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a WhenToVisitIDs");
        return validator;
    }
}

