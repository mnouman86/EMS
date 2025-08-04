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

namespace CleanArc.Application.Features.ContactForm.Commands.CreateContactResponseCommand;
public record CreateContactResponseCommand(int ContactFormId,int FeedbackStatusId, string FullName, string Remards, int CreatedBy, int CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreateContactResponseCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateContactResponseCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateContactResponseCommand> validator)
    {
        
        validator.RuleFor(c => c.Remards)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter remarks");
        return validator;
    }
}

