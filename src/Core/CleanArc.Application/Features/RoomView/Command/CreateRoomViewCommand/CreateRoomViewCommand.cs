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

namespace CleanArc.Application.Features.RoomView.Command.CreateRoomViewCommand
{
    public record CreateRoomViewCommand(string? Name, string? Description, int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreateRoomViewCommand>
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public IValidator<CreateRoomViewCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateRoomViewCommand> validator)
        {
            validator.RuleFor(c => c.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Please enter a valid Name");
            return validator;
        }
    }


}
