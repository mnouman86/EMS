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

namespace CleanArc.Application.Features.CampaignTargetItems.Command.UpdateCampaignTargetItemsCommand;

public record UpdateCampaignTargetItemsCommand(int ID, int CampaignTargetID,
string CampaignItemIDs,
bool ApplyAllItem,
int? UpdatedBy) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<UpdateCampaignTargetItemsCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateCampaignTargetItemsCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateCampaignTargetItemsCommand> validator)
    {
        validator.RuleFor(c => c.CampaignTargetID)
    .NotEmpty()
    .NotNull()
    .WithMessage("Please enter a valid Title");
        validator.RuleFor(c => c.CampaignItemIDs)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a Description");
        
        return validator;
    }
}
