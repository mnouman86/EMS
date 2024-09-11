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

namespace CleanArc.Application.Features.KBDescription.Commands.CreateKBDescriptionCommand;
public record CreateKBDescriptionCommand(
    int? KBDetailID,
    string SubHeading,
    string Content,
    string? KBContentType,
    int? CreatedBy,
    int? CultureId) : IRequest<OperationResult<bool>>,
    IValidatableModel<CreateKBDescriptionCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateKBDescriptionCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateKBDescriptionCommand> validator)
    {
        
        validator.RuleFor(c => c.KBContentType)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a KBContentType");
        validator.RuleFor(c => c.KBDetailID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a KBDetailID");
        //validator.RuleFor(c => c.SubHeading)
        //    .NotEmpty()
        //    .NotNull()
        //    .WithMessage("Please enter a SubHeading ");
        validator.RuleFor(c => c.Content)
          .NotEmpty()
          .NotNull()
          .WithMessage("Please enter a Content ");
        return validator;
    }
}

