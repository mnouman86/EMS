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
using CleanArc.Application.Models.Request;using System.Text.Json.Serialization; using CleanArc.Domain.Common;

namespace CleanArc.Application.Features.BusinessType.Command.UpdateBusinessTypeCommand;

public record UpdateBusinessTypeCommand(int Id, string? BusinessTypeName, int? UpdatedBy) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<UpdateBusinessTypeCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateBusinessTypeCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateBusinessTypeCommand> validator)
    {
        validator.RuleFor(c => c.BusinessTypeName)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please select a business type");
      
        return validator;
    }
}

