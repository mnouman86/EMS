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

namespace CleanArc.Application.Features.UserType.Command.CreateUserTypeCommand;

public record CreateUserTypeCommand(string? Title, string? Description, int? NoOfBookings, decimal? DiscountPercentage, decimal? DiscountCap, int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreateUserTypeCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateUserTypeCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateUserTypeCommand> validator)
    {
        validator.RuleFor(c => c.Title)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid Name");
        return validator;
    }
}
