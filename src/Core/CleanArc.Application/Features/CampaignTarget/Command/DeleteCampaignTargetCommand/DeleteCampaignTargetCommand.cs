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

namespace CleanArc.Application.Features.CampaignTarget.Command.DeleteCampaignTargetCommand;

public record DeleteCampaignTargetCommand(DeleteRequest deleteRequest) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<DeleteCampaignTargetCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<DeleteCampaignTargetCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<DeleteCampaignTargetCommand> validator)
    {
        validator.RuleFor(c => c.deleteRequest.SelectedIds)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please select Record");

        return validator;
    }
}
