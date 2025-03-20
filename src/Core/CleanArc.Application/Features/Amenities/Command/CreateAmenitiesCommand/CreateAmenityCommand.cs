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

namespace CleanArc.Application.Features.Amenities.Command.CreateAmenitiesCommand;
//serviceID, ServiceCategoryID, Name, Desc, Icon
public record CreateAmenityCommand(string? Name, string? Description, int? ServiceId,int? ServiceCategoryId, string? Icon, int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreateAmenityCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateAmenityCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateAmenityCommand> validator)
    {
        validator.RuleFor(c => c.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid Name");
        validator.RuleFor(c => c.ServiceId)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please select a Service");
        validator.RuleFor(c => c.ServiceCategoryId)
          .NotEmpty()
          .NotNull()
          .WithMessage("Please select a Service Category");
        return validator;
    }
}
