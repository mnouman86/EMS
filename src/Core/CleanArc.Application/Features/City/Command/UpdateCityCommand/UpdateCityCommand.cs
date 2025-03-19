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

namespace CleanArc.Application.Features.City.Command.UpdateCityCommand
{
    public record UpdateCityCommand(int ID, String? Name, string? Description, int? StateID, int? UpdatedBy, bool? IsMain) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<UpdateCityCommand>
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public IValidator<UpdateCityCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateCityCommand> validator)
        {
            validator.RuleFor(c => c.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Please enter a valid Name");
            validator.RuleFor(c => c.Description)
                .NotEmpty()
                .NotNull()
                .WithMessage("Please enter a Description");
            validator.RuleFor(c => c.StateID)
   .NotEmpty()
   .NotNull()
   .WithMessage("Please enter a valid StateID");
            //validator.RuleFor(c => c.ImagePath)
            //   .NotEmpty()
            //   .NotNull()
            //   .WithMessage("Please enter a valid ImagePath");
            //validator.RuleFor(c => c.ImageTitle)
            //   .NotEmpty()
            //   .NotNull()
            //   .WithMessage("Please enter a valid ImageTitle");
            return validator;
        }
    }
}
