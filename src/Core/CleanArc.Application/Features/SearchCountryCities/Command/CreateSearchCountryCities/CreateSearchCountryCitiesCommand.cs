using CleanArc.Application.Features.SearchHotel.Commands.CreateSearchHotelCommand;
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
using CleanArc.Application.Models.Request;using System.Text.Json.Serialization; using CleanArc.Domain.Common;

namespace CleanArc.Application.Features.SearchCountryCities.Command.CreateSearchCountryCities;

public record CreateSearchCountryCitiesCommand( String CityName ) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreateSearchCountryCitiesCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateSearchCountryCitiesCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateSearchCountryCitiesCommand> validator)
    {
        validator.RuleFor(c => c.CityName)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid Name");
        //validator.RuleFor(c => c.CityDescription)
        //    .NotEmpty()
        //    .NotNull()
        //    .WithMessage("Please enter a Description");
        return validator;
    }
}
