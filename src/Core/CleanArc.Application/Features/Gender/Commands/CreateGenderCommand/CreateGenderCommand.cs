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

namespace CleanArc.Application.Features.Gender.Commands.CreateGenderCommand;
public record CreateGenderCommand(string? Name, string? Description,int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreateGenderCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateGenderCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateGenderCommand> validator)
    {
        validator.RuleFor(c => c.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid Name");
        return validator;
    }
}

