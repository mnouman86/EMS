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

namespace CleanArc.Application.Features.CoreArea.Commands.DeleteCoreAreaCommand;

public record DeleteCoreAreaCommand(string SelectedIds, int? UpdatedBy, int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<DeleteCoreAreaCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<DeleteCoreAreaCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<DeleteCoreAreaCommand> validator)
    {
        validator.RuleFor(c => c.SelectedIds)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please select Record");
       
        return validator;
    }
}

//public record class DeleteCoreAreaCommand
//{
//}
