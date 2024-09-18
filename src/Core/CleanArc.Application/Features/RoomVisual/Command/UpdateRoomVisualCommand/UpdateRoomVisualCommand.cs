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

namespace CleanArc.Application.Features.RoomVisual.Command.UpdateRoomVisualCommand
{
    public record UpdateRoomVisualCommand(int ID, int? HotelID, int? RoomID, int? CategoryID, String? ImagePath, string? ImageTitle, bool? IsMain, int? UpdatedBy) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<UpdateRoomVisualCommand>
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public IValidator<UpdateRoomVisualCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateRoomVisualCommand> validator)
        {
            validator.RuleFor(c => c.HotelID)
             .NotEmpty()
             .NotNull()
             .WithMessage("Please enter a valid HotelID");
            validator.RuleFor(c => c.RoomID)
                .NotEmpty()
                .NotNull()
                .WithMessage("Please enter a RoomID");
            validator.RuleFor(c => c.CategoryID)
               .NotEmpty()
               .NotNull()
               .WithMessage("Please enter a CategoryID");
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
