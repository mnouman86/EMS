using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CleanArc.Application.Models.Request;using System.Text.Json.Serialization; using CleanArc.Domain.Common;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.BusinessBankAccount.Command.UpdateBusinessBankAccountCommand
{
    public record UpdateBusinessBankAccountCommand(int Id, string? AccountTitle, int? BankID, int? BusinessID, string? IBAN,int? UpdatedBy) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<UpdateBusinessBankAccountCommand>
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public IValidator<UpdateBusinessBankAccountCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateBusinessBankAccountCommand> validator)
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

}
