using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using CleanArc.SharedKernel.ValidationBase;
using FluentValidation;
using System.Text.Json.Serialization; using CleanArc.Domain.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.Currency.Commands.CreateCurrencyCommand;
public record CreateCurrencyCommand(
   string? Name,
   int? CurrencyCode,
   int? Rate,
     int? CreatedBy,
    int? CultureId,
    int? Code,
    string? Message
    ) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreateCurrencyCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateCurrencyCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateCurrencyCommand> validator)
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

