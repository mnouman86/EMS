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

namespace CleanArc.Application.Features.ActivityGroup.Commands.UpdateActivityGroupCommand;
public record UpdateActivityGroupCommand(int Id, int? ActivityID,  int? Size,int? UpdatedBy,int CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<UpdateActivityGroupCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateActivityGroupCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateActivityGroupCommand> validator)
    {
        validator.RuleFor(c => c.ActivityID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid ActivityID");
        //validator.RuleFor(c => c.Title)
        //    .NotEmpty()
        //    .NotNull()
        //    .WithMessage("Please enter a Title");
        //validator.RuleFor(c => c.From)
        // .NotEmpty()
        // .NotNull()
        // .WithMessage("Please enter a From");
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

