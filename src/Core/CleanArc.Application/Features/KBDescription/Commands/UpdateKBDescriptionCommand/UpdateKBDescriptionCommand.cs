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

namespace CleanArc.Application.Features.KBDescription.Commands.UpdateKBDescriptionCommand;
public record UpdateKBDescriptionCommand(int ID, int? KBDetailID,
string SubHeading,
string Content,
int KBContentType, int? UpdatedBy, int? CultureId) : IRequest<OperationResult<bool>>,
    IValidatableModel<UpdateKBDescriptionCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateKBDescriptionCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateKBDescriptionCommand> validator)
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

