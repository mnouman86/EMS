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

namespace CleanArc.Application.Features.KBCRelatedUrlLink.Commands.UpdateKBCRelatedUrlLinkCommand;
public record UpdateKBCRelatedUrlLinkCommand(int ID,String? Title, string? Description, string? URL, int? UpdatedBy, int? CultureId) : IRequest<OperationResult<bool>>,
    IValidatableModel<UpdateKBCRelatedUrlLinkCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateKBCRelatedUrlLinkCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateKBCRelatedUrlLinkCommand> validator)
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

