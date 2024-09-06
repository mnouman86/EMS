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

namespace CleanArc.Application.Features.KBCAttraction.Commands.CreateKBCAttractionCommand;
public record CreateKBCAttractionCommand(string? Title, string? Description,int? CreatedBy, int? CultureId) : IRequest<OperationResult<bool>>,
    IValidatableModel<CreateKBCAttractionCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateKBCAttractionCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateKBCAttractionCommand> validator)
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

