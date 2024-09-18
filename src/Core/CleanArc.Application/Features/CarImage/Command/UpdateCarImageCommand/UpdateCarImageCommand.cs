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
using System.Text.Json.Serialization; using CleanArc.Domain.Common;

namespace CleanArc.Application.Features.CarImage.Command.UpdateCarImageCommand
{
    public record UpdateCarImageCommand(int ID, int? BusinessID, int? CarID, string? ImagePath, string? ImageTitle, bool? IsMain, int? UpdatedBy) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<UpdateCarImageCommand>
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public IValidator<UpdateCarImageCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateCarImageCommand> validator)
        {
            validator.RuleFor(c => c.BusinessID)
             .NotEmpty()
             .NotNull()
             .WithMessage("Please enter a valid BusinessID");
            validator.RuleFor(c => c.CarID)
                .NotEmpty()
                .NotNull()
                .WithMessage("Please enter a CarID");
            validator.RuleFor(c => c.ImagePath)
               .NotEmpty()
               .NotNull()
               .WithMessage("Please enter a valid ImagePath");
            validator.RuleFor(c => c.ImageTitle)
                .NotEmpty()
                .NotNull()
                .WithMessage("Please enter a ImageTitle");

            return validator;
        }
    }
}
