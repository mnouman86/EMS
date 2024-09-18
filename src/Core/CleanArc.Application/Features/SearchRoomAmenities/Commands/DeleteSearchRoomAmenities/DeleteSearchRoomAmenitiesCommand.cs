using CleanArc.Application.Features.SearchHotelRoomDetail.Command.DeleteSearchHotelRoomDetail;
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

namespace CleanArc.Application.Features.SearchRoomAmenities.Commands.DeleteSearchRoomAmenities;

public record DeleteSearchRoomAmenitiesCommand(string SelectedIds, int? UpdatedBy) : IRequest<OperationResult<ResponseEntity>>,
IValidatableModel<DeleteSearchRoomAmenitiesCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<DeleteSearchRoomAmenitiesCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<DeleteSearchRoomAmenitiesCommand> validator)
    {
        validator.RuleFor(c => c.SelectedIds)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please select Record");

        return validator;
    }
}
