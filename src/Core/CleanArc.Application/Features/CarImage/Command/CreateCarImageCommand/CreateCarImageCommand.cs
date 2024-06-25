using CleanArc.Application.Features.AgeType.Commands.CreateAgeTypeCommand;
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

namespace CleanArc.Application.Features.CarImage.Command.CreateCarImageCommand;

public record CreateCarImageCommand(int? BusinessID, int? CarID,  String? ImagePath, string? ImageTitle, bool? IsMain, int? CreatedBy) : IRequest<OperationResult<bool>>,
    IValidatableModel<CreateCarImageCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateCarImageCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateCarImageCommand> validator)
    {
        validator.RuleFor(c => c.BusinessID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid BusinessID");
        validator.RuleFor(c => c.CarID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a CarID");
        validator.RuleFor(c => c.ImagePath)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a valid ImagePath");
        validator.RuleFor(c => c.ImageTitle)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a ImageTitle");
        
        return validator;
    }
}
