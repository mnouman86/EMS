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
using CleanArc.Application.Models.Request;
using System.Text.Json.Serialization; 
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.HotelImage;

namespace CleanArc.Application.Features.HotelImage.Command.CreateHotelImageCommand;

public record CreateHotelImageCommand(int? GenericTitleId, int? ServiceTypeEnumId, int? CultureId, List<ImageData>? ImageData) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreateHotelImageCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateHotelImageCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateHotelImageCommand> validator)
    {
        
        validator.RuleFor(c => c.ImageData)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a Image Data");
        
        return validator;
    }
}
