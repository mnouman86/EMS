using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CleanArc.Application.Models.Request;using System.Text.Json.Serialization; using CleanArc.Domain.Common;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.MappingRoomImage.Command.DeleteMappingRoomImageCommand;

public record DeleteMappingRoomImageCommand(DeleteRequest deleteRequest) : IRequest<OperationResult<ResponseEntity>>,
IValidatableModel<DeleteMappingRoomImageCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<DeleteMappingRoomImageCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<DeleteMappingRoomImageCommand> validator)
    {
        validator.RuleFor(c => c.deleteRequest.SelectedIds)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please select Record");

        return validator;
    }
}
