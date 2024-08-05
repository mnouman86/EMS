using CleanArc.Application.Features.AgeType.Commands.UpdateAgeTypeCommand;
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

namespace CleanArc.Application.Features.BusinessType.Command.UpdateBusinessTypeCommand;

public record UpdateBusinessTypeCommand(int ID, bool? Company, bool? IndividualPerson, int? UpdatedBy) : IRequest<OperationResult<bool>>,
    IValidatableModel<UpdateBusinessTypeCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateBusinessTypeCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateBusinessTypeCommand> validator)
    {
        validator.RuleFor(c => c.Company)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid Company");
        validator.RuleFor(c => c.IndividualPerson)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a IndividualPerson");
      
        return validator;
    }
}

