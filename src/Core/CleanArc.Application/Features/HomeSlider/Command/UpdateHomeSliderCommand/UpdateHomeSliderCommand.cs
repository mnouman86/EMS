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
using System.Text.Json.Serialization; using CleanArc.Domain.Common;

namespace CleanArc.Application.Features.HomeSlider.Command.UpdateHomeSliderCommand;

public record UpdateHomeSliderCommand(int ID, string? Title,
string? Description,
string? Url,
//string EndDate,
string? Image,
int? UpdatedBy) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<UpdateHomeSliderCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateHomeSliderCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateHomeSliderCommand> validator)
    {
        validator.RuleFor(c => c.Title)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a valid Title");
        validator.RuleFor(c => c.Description)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a Description");
        validator.RuleFor(c => c.Url)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a valid Url");
        validator.RuleFor(c => c.Image)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a Image");
        return validator;
    }
}
