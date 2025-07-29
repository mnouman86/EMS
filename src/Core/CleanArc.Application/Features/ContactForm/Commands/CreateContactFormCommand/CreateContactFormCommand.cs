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

namespace CleanArc.Application.Features.ContactForm.Commands.CreateContactFormCommand;
public record CreateContactFormCommand(string FullName, string Email, string Subject, string Message, int CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreateContactFormCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateContactFormCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateContactFormCommand> validator)
    {
        
        validator.RuleFor(c => c.FullName)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter Full Name");
        validator.RuleFor(c => c.Email)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter an Email");
        validator.RuleFor(c => c.Message)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a Message");
        return validator;
    }
}

