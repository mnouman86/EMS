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

namespace CleanArc.Application.Features.ActivityGroup.Commands.CreateActivityGroupCommand;
public record CreateActivityGroupCommand(int? ActivityID,  int? Size, int? CreatedBy, int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreateActivityGroupCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateActivityGroupCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateActivityGroupCommand> validator)
    {
        validator.RuleFor(c => c.ActivityID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid ActivityID");
        //validator.RuleFor(c => c.From)
        //    .NotEmpty()
        //    .NotNull()
        //    .WithMessage("Please enter a From");
        //validator.RuleFor(c => c.To)
        //   .NotEmpty()
        //   .NotNull()
        //   .WithMessage("Please enter a To");
        validator.RuleFor(c => c.Size)
          .NotEmpty()
          .NotNull()
          .WithMessage("Please enter a Size");
        return validator;
    }
}

