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

namespace CleanArc.Application.Features.KBDetail.Commands.DeleteKBDetailCommand;

public record DeleteKBDetailCommand(string SelectedIds, int? UpdatedBy, int? CultureId) : IRequest<OperationResult<bool>>,
    IValidatableModel<DeleteKBDetailCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<DeleteKBDetailCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<DeleteKBDetailCommand> validator)
    {
        validator.RuleFor(c => c.SelectedIds)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please select Record");
       
        return validator;
    }
}

//public record class DeleteKBDetailCommand
//{
//}
