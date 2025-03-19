using CleanArc.Application.Features.AgeType.Commands.UpdateAgeTypeCommand;
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

namespace CleanArc.Application.Features.SearchFilterStay.Commands.UpdateSearchFilterStayCommand;

public record UpdateSearchFilterStayCommand(int ID, int HotelID, int CityID, String Name, String CityName, String CityDescription, String RoomTypeName, String RoomTypeDescription, Decimal RoomDetailPrice) : IRequest<OperationResult<ResponseEntity>>,
IValidatableModel<UpdateSearchFilterStayCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateSearchFilterStayCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateSearchFilterStayCommand> validator)
    {
        validator.RuleFor(c => c.Name)
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
