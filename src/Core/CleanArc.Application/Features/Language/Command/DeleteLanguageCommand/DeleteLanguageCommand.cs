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
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.Language.Command.DeleteLanguageCommand;

public record DeleteLanguageCommand(string SelectedIds, int? UpdatedBy) : IRequest<OperationResult<bool>>,
    IValidatableModel<DeleteLanguageCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<DeleteLanguageCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<DeleteLanguageCommand> validator)
    {
        validator.RuleFor(c => c.SelectedIds)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please select Record");

        return validator;
    }
}
