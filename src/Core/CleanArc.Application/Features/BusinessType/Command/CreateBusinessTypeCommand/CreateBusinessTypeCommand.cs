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

namespace CleanArc.Application.Features.BusinessType.Command.CreateBusinessTypeCommand;

public record CreateBusinessTypeCommand(bool? Company,string? Name, bool? IndividualPerson, int? CreatedBy) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreateBusinessTypeCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateBusinessTypeCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateBusinessTypeCommand> validator)
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

