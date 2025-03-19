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

namespace CleanArc.Application.Features.Currency.Commands.UpdateCurrencyCommand;
public record UpdateCurrencyCommand(
   int ID,
  string? Name,
  string? CurrencyCode,
   decimal? Rate,
  int? UpdatedBy) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<UpdateCurrencyCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateCurrencyCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateCurrencyCommand> validator)
    {
        validator.RuleFor(c => c.Name)
     .NotEmpty()
     .NotNull()
     .WithMessage("Please enter a valid Name");
        validator.RuleFor(c => c.CurrencyCode)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a CurrencyCode");
        validator.RuleFor(c => c.Rate)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a Rate");
        return validator;
    }
}

