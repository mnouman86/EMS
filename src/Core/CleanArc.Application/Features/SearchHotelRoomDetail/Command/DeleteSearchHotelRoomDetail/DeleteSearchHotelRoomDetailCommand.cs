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

namespace CleanArc.Application.Features.SearchHotelRoomDetail.Command.DeleteSearchHotelRoomDetail;

public record DeleteSearchHotelRoomDetailCommand(string SelectedIds, int? UpdatedBy) : IRequest<OperationResult<bool>>,
IValidatableModel<DeleteSearchHotelRoomDetailCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<DeleteSearchHotelRoomDetailCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<DeleteSearchHotelRoomDetailCommand> validator)
    {
        validator.RuleFor(c => c.SelectedIds)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please select Record");

        return validator;
    }
}