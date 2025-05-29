using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CleanArc.Application.Models.Request;using System.Text.Json.Serialization; using CleanArc.Domain.Common;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.Manufacturer.Command.UpdateManufacturerCommand;

public record UpdateManufacturerCommand(int Id, string? Name, string? Description, int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
IValidatableModel<UpdateManufacturerCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateManufacturerCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateManufacturerCommand> validator)
    {
        validator.RuleFor(c => c.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid Name");
        return validator;
    }
}
