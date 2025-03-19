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
public record UpdateFAQsCommand(int ID,int? CategoryServiceID, int? ServiceID, int? SubServiceID, string? Question, string? Answer, int? UpdatedBy, int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<UpdateFAQsCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateFAQsCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateFAQsCommand> validator)
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

