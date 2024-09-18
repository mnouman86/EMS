using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization; using CleanArc.Domain.Common;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.MappingRoomImage.Command.CreateMappingRoomImageCommand;

public record CreateMappingRoomImageCommand(int? RoomID, int? CategoryID, string? ImagePaths , string? ImageTitles, string? IsMains, int? CreatedBy) : IRequest<OperationResult<ResponseEntity>>,
IValidatableModel<CreateMappingRoomImageCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateMappingRoomImageCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateMappingRoomImageCommand> validator)
    {
        validator.RuleFor(c => c.RoomID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid RoomID");
        validator.RuleFor(c => c.CategoryID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a CategoryID");
        validator.RuleFor(c => c.ImagePaths)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a ImagePaths");
        validator.RuleFor(c => c.ImageTitles)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a ImageTitles");
        return validator;
    }

}