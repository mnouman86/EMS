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

namespace CleanArc.Application.Features.FAQs.Commands.UpdateFAQsCommand;
public record UpdateFAQsCommand(int Id, string? Question,
    string? Answer, int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<UpdateFAQsCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateFAQsCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateFAQsCommand> validator)
    {
        validator.RuleFor(c => c.Question)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a Question.");
        validator.RuleFor(c => c.Answer)
    .NotEmpty()
    .NotNull()
    .WithMessage("Please enter an Answer.");
        return validator;
    }
}

