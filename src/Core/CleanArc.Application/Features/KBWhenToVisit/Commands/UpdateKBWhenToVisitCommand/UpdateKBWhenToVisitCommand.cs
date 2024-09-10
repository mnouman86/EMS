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

namespace CleanArc.Application.Features.KBWhenToVisit.Commands.UpdateKBWhenToVisitCommand;
public record UpdateKBWhenToVisitCommand( int ID,String? Title, string? Description, int? UpdatedBy, int? CultureId) : IRequest<OperationResult<bool>>,
    IValidatableModel<UpdateKBWhenToVisitCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateKBWhenToVisitCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateKBWhenToVisitCommand> validator)
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

