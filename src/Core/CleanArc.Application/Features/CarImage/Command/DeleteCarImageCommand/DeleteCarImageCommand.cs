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

namespace CleanArc.Application.Features.CarImage.Command.DeleteCarImageCommand;

public record DeleteCarImageCommand(string SelectedIds, int? UpdatedBy, int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
IValidatableModel<DeleteCarImageCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<DeleteCarImageCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<DeleteCarImageCommand> validator)
    {
        validator.RuleFor(c => c.SelectedIds)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please select Record");

        return validator;
    }
}

//public record class DeleteAgeTypeCommand
//{
//}
