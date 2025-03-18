using CleanArc.Application.Features.AgeType.Commands.DeleteAgeTypeCommand;
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

namespace CleanArc.Application.Features.Service.Command.DeleteServiceCommand;

public record DeleteServiceCommand(string SelectedIds, int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<DeleteServiceCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<DeleteServiceCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<DeleteServiceCommand> validator)
    {
        validator.RuleFor(c => c.SelectedIds)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid record ID.");

        return validator;
    }
}

