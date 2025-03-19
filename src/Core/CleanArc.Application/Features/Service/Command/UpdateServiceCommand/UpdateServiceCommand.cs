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
using Azure.Core;

namespace CleanArc.Application.Features.Service.Command.UpdateServiceCommand
{
    public record UpdateServiceCommand(int ID, string? Name, string? Description, int? CultureId, string? Icon) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<UpdateServiceCommand>
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public IValidator<UpdateServiceCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateServiceCommand> validator)
        {
            validator.RuleFor(c => c.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid Service Name");
            validator.RuleFor(c => c.Icon)
                .NotEmpty()
                .NotNull()
                .WithMessage("Please enter a valid Google Icon code from https://fonts.google.com/icons");
            return validator;
        }
    }
}
