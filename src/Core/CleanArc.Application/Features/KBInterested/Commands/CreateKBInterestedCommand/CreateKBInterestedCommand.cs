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

namespace CleanArc.Application.Features.KBInterested.Commands.CreateKBInterestedCommand;
public record CreateKBInterestedCommand(string? Type, string? Name, string? Description, int? CreatedBy, int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreateKBInterestedCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateKBInterestedCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateKBInterestedCommand> validator)
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

