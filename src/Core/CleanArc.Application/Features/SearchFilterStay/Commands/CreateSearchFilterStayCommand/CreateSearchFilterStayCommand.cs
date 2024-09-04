using CleanArc.Application.Features.AgeType.Commands.CreateAgeTypeCommand;
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

namespace CleanArc.Application.Features.SearchFilterStay.Commands.CreateSearchFilterStayCommand;

public record CreateSearchFilterStayCommand( int HotelID, int CityID, String HotelName, String CityName, String CityDescription, String RoomTypeName, String RoomTypeDescription, Decimal RoomDetailPrice) : IRequest<OperationResult<bool>>,
    IValidatableModel<CreateSearchFilterStayCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateSearchFilterStayCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateSearchFilterStayCommand> validator)
    {
        validator.RuleFor(c => c.HotelName)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid Name");
        validator.RuleFor(c => c.CityDescription)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a Description");
        return validator;
    }
}
