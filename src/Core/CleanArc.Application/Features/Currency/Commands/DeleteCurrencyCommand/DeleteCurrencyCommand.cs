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

namespace CleanArc.Application.Features.Currency.Commands.DeleteCurrencyCommand;

public record DeleteCurrencyCommand(string SelectedIds, int? UpdatedBy, int? CultureId) : IRequest<OperationResult<bool>>,
    IValidatableModel<DeleteCurrencyCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<DeleteCurrencyCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<DeleteCurrencyCommand> validator)
    {
        validator.RuleFor(c => c.SelectedIds)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please select Record");
       
        return validator;
    }
}

//public record class DeleteCurrencyCommand
//{
//}
