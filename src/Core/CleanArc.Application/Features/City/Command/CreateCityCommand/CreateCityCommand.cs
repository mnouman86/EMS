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
using System.Text.Json.Serialization; using CleanArc.Domain.Common;

namespace CleanArc.Application.Features.City.Command.CreateCityCommand;

public record CreateCityCommand(string? Name, string? Description, int? StateID,bool? IsMain,  int? CreatedBy,  string? ImagePath , string? ImageTitle ) : IRequest<OperationResult<ResponseEntity>>,
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
        //validator.RuleFor(c => c.ImagePath)
        //   .NotEmpty()
        //   .NotNull()
        //   .WithMessage("Please enter a valid ImagePath");
        //validator.RuleFor(c => c.ImageTitle)
        //   .NotEmpty()
        //   .NotNull()
        //   .WithMessage("Please enter a valid ImageTitle");
        //validator.RuleFor(c => c.ImagePath)
        //  .Must(path => string.IsNullOrEmpty(path) || !string.IsNullOrEmpty(path))
        //  .WithMessage("Please enter a valid ImagePath");

        //validator.RuleFor(c => c.ImageTitle)
        //    .Must(title => string.IsNullOrEmpty(title) || !string.IsNullOrEmpty(title))
        //    .WithMessage("Please enter a valid ImageTitle");

        return validator;
    }

   
}
