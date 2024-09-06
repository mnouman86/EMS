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

namespace CleanArc.Application.Features.KBCRelatedUrlLink.Commands.CreateKBCRelatedUrlLinkCommand;
public record CreateKBCRelatedUrlLinkCommand(string? Title, string? Description, string? URL, int? CreatedBy, int? CultureId) : IRequest<OperationResult<bool>>,
    IValidatableModel<CreateKBCRelatedUrlLinkCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateKBCRelatedUrlLinkCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateKBCRelatedUrlLinkCommand> validator)
    {
        validator.RuleFor(c => c.Title)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid Title");
        validator.RuleFor(c => c.Description)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a Description");
        validator.RuleFor(c => c.URL)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a URL");
        return validator;
    }
}

