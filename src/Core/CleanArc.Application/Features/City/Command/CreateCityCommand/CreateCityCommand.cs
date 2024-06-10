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

namespace CleanArc.Application.Features.City.Command.CreateCityCommand;

public record CreateCityCommand(string? Name, string? Description, int? StateID, string? ImagePath, string? ImageTitle, int? CreatedBy, bool? IsMain) : IRequest<OperationResult<bool>>,
    IValidatableModel<CreateCityCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateCityCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateCityCommand> validator)
    {
        validator.RuleFor(c => c.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid Name");
        validator.RuleFor(c => c.Description)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid Description");
        validator.RuleFor(c => c.StateID)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a valid StateID");
        validator.RuleFor(c => c.ImagePath)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a valid ImagePath");
        validator.RuleFor(c => c.ImageTitle)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a valid ImageTitle");
       
        return validator;
    }

   
}
