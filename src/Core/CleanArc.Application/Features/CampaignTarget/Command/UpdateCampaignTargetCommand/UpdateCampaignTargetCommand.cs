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

namespace CleanArc.Application.Features.CampaignTarget.Command.UpdateCampaignTargetCommand;

public record UpdateCampaignTargetCommand(int ID, int CampaignID,int GenericTitleID,int ServiceCategoryID,
int? UpdatedBy) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<UpdateCampaignTargetCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateCampaignTargetCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateCampaignTargetCommand> validator)
    {
        validator.RuleFor(c => c.CampaignID)
    .NotEmpty()
    .NotNull()
    .WithMessage("Please enter a valid CampaignID");
        validator.RuleFor(c => c.GenericTitleID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a GenericTitleID");
        validator.RuleFor(c => c.ServiceCategoryID)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a valid ServiceCategoryID");
        
        return validator;
    }
}
