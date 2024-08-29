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

namespace CleanArc.Application.Features.FAQs.Commands.CreateFAQsCommand;
public record CreateFAQsCommand(int? CategoryServiceID, int? ServiceID, int? SubServiceID, string? Question, string? Answer, int? CreatedBy, int? CultureId) : IRequest<OperationResult<bool>>,
    IValidatableModel<CreateFAQsCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateFAQsCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateFAQsCommand> validator)
    {
        validator.RuleFor(c => c.CategoryServiceID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid CategoryServiceID");
        validator.RuleFor(c => c.ServiceID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a ServiceID");
        validator.RuleFor(c => c.SubServiceID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a SubServiceID");
        validator.RuleFor(c => c.Question)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a Question");
        validator.RuleFor(c => c.Answer)
    .NotEmpty()
    .NotNull()
    .WithMessage("Please enter a QueAnswerstion");
        return validator;
    }
}

