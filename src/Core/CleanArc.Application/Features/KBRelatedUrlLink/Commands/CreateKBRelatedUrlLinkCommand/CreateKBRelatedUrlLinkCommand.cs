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

namespace CleanArc.Application.Features.KBRelatedUrlLink.Commands.CreateKBRelatedUrlLinkCommand;
public record CreateKBRelatedUrlLinkCommand(int? KBDetailID,string? Title, string? Description, string? URL, int? CreatedBy, int? CultureId) : IRequest<OperationResult<bool>>,
    IValidatableModel<CreateKBRelatedUrlLinkCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateKBRelatedUrlLinkCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateKBRelatedUrlLinkCommand> validator)
    {
        validator.RuleFor(c => c.KBDetailID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid KBDetailID");
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

