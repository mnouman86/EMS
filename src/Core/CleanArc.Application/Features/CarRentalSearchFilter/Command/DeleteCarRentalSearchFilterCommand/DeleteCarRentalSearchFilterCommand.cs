using CleanArc.Application.Features.SearchHotel.Commands.DeleteSearchHotelCommand;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization; using CleanArc.Domain.Common;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.CarRentalSearchFilter.Command.DeleteCarRentalSearchFilter;

public record DeleteCarRentalSearchFilterCommand(string SelectedIds, int? UpdatedBy) : IRequest<OperationResult<ResponseEntity>>,
IValidatableModel<DeleteCarRentalSearchFilterCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<DeleteCarRentalSearchFilterCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<DeleteCarRentalSearchFilterCommand> validator)
    {
        validator.RuleFor(c => c.SelectedIds)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please select Record");

        return validator;
    }
}