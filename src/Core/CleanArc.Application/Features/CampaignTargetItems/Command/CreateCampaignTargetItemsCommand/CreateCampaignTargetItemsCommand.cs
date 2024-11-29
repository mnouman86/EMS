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
using System.Text.Json.Serialization; using CleanArc.Domain.Common;

namespace CleanArc.Application.Features.CampaignTargetItems.Command.CreateCampaignTargetItemsCommand;

public record CreateCampaignTargetItemsCommand(int? CampaignTargetID, 
string? CampaignItemIDs,
int? CreatedBy) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreateCampaignTargetItemsCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateCampaignTargetItemsCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateCampaignTargetItemsCommand> validator)
    {
        validator.RuleFor(c => c.CampaignTargetID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid CampaignTargetID");
        validator.RuleFor(c => c.CampaignItemIDs)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a Description");
        
        return validator;
    }
}
