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
using CleanArc.Application.Models.Request;using System.Text.Json.Serialization; using CleanArc.Domain.Common;

namespace CleanArc.Application.Features.SearchHotelImage.Command.CreateSearchHotelImageCommand;

public record CreateSearchHotelImageCommand(string? HotelName, string? ImageTitle, string? ImagePath, int? HotelID) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreateSearchHotelImageCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateSearchHotelImageCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateSearchHotelImageCommand> validator)
    {
        validator.RuleFor(c => c.HotelName)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid Name");
        validator.RuleFor(c => c.ImageTitle)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a Description");
        return validator;
    }
}
