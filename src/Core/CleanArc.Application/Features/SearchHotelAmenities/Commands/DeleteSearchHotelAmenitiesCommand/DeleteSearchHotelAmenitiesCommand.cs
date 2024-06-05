using CleanArc.Application.Features.SearchHotel.Commands.DeleteSearchHotelCommand;
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

namespace CleanArc.Application.Features.SearchHotelAmenities.Commands.DeleteSearchHotelAmenitiesCommand;

public  record DeleteSearchHotelAmenitiesCommand(string SelectedIds, int? UpdatedBy) : IRequest<OperationResult<bool>>,
IValidatableModel<DeleteSearchHotelAmenitiesCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<DeleteSearchHotelAmenitiesCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<DeleteSearchHotelAmenitiesCommand> validator)
    {
        validator.RuleFor(c => c.SelectedIds)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please select Record");

        return validator;
    }
}



