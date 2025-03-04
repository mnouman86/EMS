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
using System.Text.Json.Serialization; using CleanArc.Domain.Common;

namespace CleanArc.Application.Features.Bank.Command.CreateBankCommand;

public record CreateBankCommand(string? Name, string? Description,int? CountryLookUpID, int? CreatedBy) : IRequest<OperationResult<ResponseEntity>>,
IValidatableModel<CreateBankCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateBankCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateBankCommand> validator)
    {
        validator.RuleFor(c => c.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid Name");
        validator.RuleFor(c => c.Description)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a Description");
        return validator;
    }
}
