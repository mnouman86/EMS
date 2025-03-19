using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using CleanArc.SharedKernel.ValidationBase;
using FluentValidation;
using CleanArc.Application.Models.Request;using System.Text.Json.Serialization; using CleanArc.Domain.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.ActivityAddressMapping.Commands.CreateActivityAddressMappingCommand;
public record CreateActivityAddressMappingCommand(int? ActivityID, string? ImagePath, string? ImageTitle, bool? IsMain, int? CreatedBy,int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreateActivityAddressMappingCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateActivityAddressMappingCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateActivityAddressMappingCommand> validator)
    {
        validator.RuleFor(c => c.ActivityID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid ActivityID");
        validator.RuleFor(c => c.ImagePath)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a ImagePath");
        validator.RuleFor(c => c.ImageTitle)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a ImageTitle");
      
        return validator;
    }
}

