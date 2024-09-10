using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using CleanArc.SharedKernel.ValidationBase;
using FluentValidation;
using System.Text.Json.Serialization;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.KBWhenToVisit.Commands.CreateKBWhenToVisitCommand;
public record CreateKBWhenToVisitCommand(string? Title, string? Description, int? CreatedBy, int? CultureId) : IRequest<OperationResult<bool>>,
    IValidatableModel<CreateKBWhenToVisitCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateKBWhenToVisitCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateKBWhenToVisitCommand> validator)
    {
       
        validator.RuleFor(c => c.Title)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid Title");
        validator.RuleFor(c => c.Description)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a Description");
        
        return validator;
    }
}

