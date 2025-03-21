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

namespace CleanArc.Application.Features.Section.Command.UpdateSectionCommand
{
    public  record UpdateSectionCommand(int Id, String? Name, string? Description, int? UpdatedBy) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<UpdateSectionCommand>
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public IValidator<UpdateSectionCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateSectionCommand> validator)
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
