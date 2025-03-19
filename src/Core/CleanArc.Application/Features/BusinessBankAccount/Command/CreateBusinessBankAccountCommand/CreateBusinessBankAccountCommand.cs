using CleanArc.Application.Features.AgeType.Commands.CreateAgeTypeCommand;
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

namespace CleanArc.Application.Features.BusinessBankAccount.Command.BusinessBankAccountCommand;

public record CreateBusinessBankAccountCommand(string? AccountTitle, int? BankID, int? BusinessID,string IBAN, int? CreatedBy) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreateBusinessBankAccountCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateBusinessBankAccountCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateBusinessBankAccountCommand> validator)
    {
        validator.RuleFor(c => c.AccountTitle)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid AccountTitle");
        validator.RuleFor(c => c.BankID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a BankID");
        validator.RuleFor(c => c.BusinessID)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a valid BusinessID");
        validator.RuleFor(c => c.IBAN)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a IBAN");
        return validator;
    }
}

