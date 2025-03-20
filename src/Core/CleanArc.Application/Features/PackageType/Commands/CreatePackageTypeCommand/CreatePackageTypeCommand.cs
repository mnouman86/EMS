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

namespace CleanArc.Application.Features.PackageType.Commands.CreatePackageTypeCommand;
public record CreatePackageTypeCommand(string? Title,
    int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreatePackageTypeCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreatePackageTypeCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreatePackageTypeCommand> validator)
    {
        validator.RuleFor(c => c.Title)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid Title");
       
        
        return validator;
    }
}

