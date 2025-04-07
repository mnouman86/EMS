using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using CleanArc.SharedKernel.ValidationBase;
using FluentValidation;
using CleanArc.Application.Models.Request;using System.Text.Json.Serialization; using CleanArc.Domain.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.ActivitySeason.Commands.CreateActivitySeasonCommand;
public record CreateActivitySeasonCommand(string? Name, string? Description,int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreateActivitySeasonCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateActivitySeasonCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateActivitySeasonCommand> validator)
    {
        validator.RuleFor(c => c.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid Name");
        validator.RuleFor(c => c.Description)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a Description");
        return validator;
    }
}

