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

namespace CleanArc.Application.Features.Language.Command.CreateLanguageCommand
{
    public record CreateLanguageCommand(string? Name, string? Description, int? CreatedBy) : IRequest<OperationResult<bool>>,
    IValidatableModel<CreateLanguageCommand>
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public IValidator<CreateLanguageCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateLanguageCommand> validator)
        {
            validator.RuleFor(c => c.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Please enter a valid Name");
            validator.RuleFor(c => c.Description)
                .NotEmpty()
                .NotNull()
                .WithMessage("Please enter a Description");
            return validator;
        }
    }


}
