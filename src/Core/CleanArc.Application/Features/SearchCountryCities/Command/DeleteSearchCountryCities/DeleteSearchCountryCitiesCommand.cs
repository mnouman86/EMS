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

namespace CleanArc.Application.Features.SearchCountryCities.Command.DeleteSearchCountryCities;

public record DeleteSearchCountryCitiesCommand(string SelectedIds, int? UpdatedBy) : IRequest<OperationResult<bool>>,
IValidatableModel<DeleteSearchCountryCitiesCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<DeleteSearchCountryCitiesCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<DeleteSearchCountryCitiesCommand> validator)
    {
        validator.RuleFor(c => c.SelectedIds)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please select Record");

        return validator;
    }
}
