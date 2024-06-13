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
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.SearchBusinessCarDetail.Command.DeleteSearchBusinessCarDetail;

public record DeleteSearchBusinessCarDetailCommand(string SelectedIds, int? UpdatedBy) : IRequest<OperationResult<bool>>,
IValidatableModel<DeleteSearchBusinessCarDetailCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<DeleteSearchBusinessCarDetailCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<DeleteSearchBusinessCarDetailCommand> validator)
    {
        validator.RuleFor(c => c.SelectedIds)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please select Record");

        return validator;
    }
}