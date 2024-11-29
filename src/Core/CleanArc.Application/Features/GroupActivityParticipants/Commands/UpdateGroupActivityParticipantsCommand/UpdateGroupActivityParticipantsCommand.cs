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

namespace CleanArc.Application.Features.GroupActivityParticipants.Commands.UpdateGroupActivityParticipantsCommand;
public record UpdateGroupActivityParticipantsCommand(int ID,
  //int? GenericTitleID,
  string MobileNumber,
  string Email,
int? GroupActivityID,
string FirstName,
string LastName,
bool? Lead,
    int? UpdatedBy, 
    int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<UpdateGroupActivityParticipantsCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateGroupActivityParticipantsCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateGroupActivityParticipantsCommand> validator)
    {
       
        validator.RuleFor(c => c.GroupActivityID)
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

