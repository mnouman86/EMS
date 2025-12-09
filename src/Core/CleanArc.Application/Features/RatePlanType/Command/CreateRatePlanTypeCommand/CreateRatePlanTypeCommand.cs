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

namespace CleanArc.Application.Features.RatePlanType.Command.CreateRatePlanTypeCommand
{
    public record CreateRatePlanTypeCommand(int? RoomDetailId, int? GuestQuantity, decimal? DefaultRate, string? Description,string RatePlanName, int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreateRatePlanTypeCommand>
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public IValidator<CreateRatePlanTypeCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateRatePlanTypeCommand> validator)
        {
            validator.RuleFor(c => c.RatePlanName)
                .NotEmpty()
                .NotNull()
                .WithMessage("Please enter a valid Name");
            return validator;
        }
    }


}
