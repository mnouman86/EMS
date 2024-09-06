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

namespace CleanArc.Application.Features.KBCRelatedUrlLink.Commands.DeleteKBCRelatedUrlLinkCommand;

public record DeleteKBCRelatedUrlLinkCommand(string SelectedIds, int? UpdatedBy, int? CultureId) : IRequest<OperationResult<bool>>,
    IValidatableModel<DeleteKBCRelatedUrlLinkCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<DeleteKBCRelatedUrlLinkCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<DeleteKBCRelatedUrlLinkCommand> validator)
    {
        validator.RuleFor(c => c.SelectedIds)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please select Record");
       
        return validator;
    }
}

//public record class DeleteKBCRelatedUrlLinkCommand
//{
//}
