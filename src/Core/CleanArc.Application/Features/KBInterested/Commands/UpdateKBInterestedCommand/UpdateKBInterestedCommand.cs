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

namespace CleanArc.Application.Features.KBInterested.Commands.UpdateKBInterestedCommand;
public record UpdateKBInterestedCommand(int ID,string? Type, string? Name, string? Description, int? UpdatedBy, int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<UpdateKBInterestedCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateKBInterestedCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateKBInterestedCommand> validator)
    {

        validator.RuleFor(c => c.Type)
     .NotEmpty()
     .NotNull()
     .WithMessage("Please enter a valid Type");
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

