//using CleanArc.Application.Features.AgeType.Commands.DeleteAgeTypeCommand;
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

namespace CleanArc.Application.Features.Hotel.Command.DeleteHotelCommand;

public record DeleteHotelCommand(string SelectedIds, int? UpdatedBy, int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<DeleteHotelCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<DeleteHotelCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<DeleteHotelCommand> validator)
    {
        validator.RuleFor(c => c.SelectedIds)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please select Record");

        return validator;
    }
}
