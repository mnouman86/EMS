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
using System.Text.Json.Serialization; using CleanArc.Domain.Common;

namespace CleanArc.Application.Features.SearchCarAmenities.Commands.DeleteSearchCarAmenitiesCommand;

public  record DeleteSearchCarAmenitiesCommand(string SelectedIds, int? UpdatedBy) : IRequest<OperationResult<ResponseEntity>>,
IValidatableModel<DeleteSearchCarAmenitiesCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<DeleteSearchCarAmenitiesCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<DeleteSearchCarAmenitiesCommand> validator)
    {
        validator.RuleFor(c => c.SelectedIds)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please select Record");

        return validator;
    }
}



