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

namespace CleanArc.Application.Features.ContactForm.Commands.UpdateContactFormCommand;
public record UpdateContactFormCommand(int Id, string FullName, string Email, string Subject, string Message, int CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<UpdateContactFormCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateContactFormCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateContactFormCommand> validator)
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

