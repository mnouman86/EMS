using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using CleanArc.SharedKernel.ValidationBase;
using FluentValidation;
using CleanArc.Application.Models.Request;using System.Text.Json.Serialization; using CleanArc.Domain.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.GroupActivityParticipants.Commands.CreateGroupActivityParticipantsCommand;
public record CreateGroupActivityParticipantsCommand(
 //int? GenericTitleID,
 string? guid,
 int? GenericTitleId,
 int? GroupTypeId,
 string? MobileNumber,
 string? Email,
 string FirstName,
 string LastName,
 bool? Lead,
    int? GroupSize,
    int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreateGroupActivityParticipantsCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateGroupActivityParticipantsCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateGroupActivityParticipantsCommand> validator)
    {
        //validator.RuleFor(c => c.GenericTitleID)
        //    .NotEmpty()
        //    .NotNull()
        //    .WithMessage("Please enter a valid GenericTitleID");
        validator.RuleFor(c => c.GenericTitleId)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a GroupActivityID");
        validator.RuleFor(c => c.FirstName)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a FirstName");
        validator.RuleFor(c => c.LastName)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a LastName");
        validator.RuleFor(c => c.Lead)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a Lead");
       
        return validator;
    }
}

