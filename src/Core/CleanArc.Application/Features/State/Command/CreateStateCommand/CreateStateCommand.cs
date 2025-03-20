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
using CleanArc.Application.Models.Request;using System.Text.Json.Serialization; using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.Country;


namespace CleanArc.Application.Features.State.Command.CreateStateCommand;

public record CreateStateCommand(string? Name, string? Description, int? CountryLookUpId , int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreateStateCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateStateCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateStateCommand> validator)
    {
        validator.RuleFor(c => c.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid Name");
        return validator;
    }
}
