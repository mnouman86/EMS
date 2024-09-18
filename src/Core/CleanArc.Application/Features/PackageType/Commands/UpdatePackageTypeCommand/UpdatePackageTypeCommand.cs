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

namespace CleanArc.Application.Features.PackageType.Commands.UpdatePackageTypeCommand;
public record UpdatePackageTypeCommand(int ID,
    string? Title,
    int? CultureId,
     int? UpdatedBy) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<UpdatePackageTypeCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdatePackageTypeCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdatePackageTypeCommand> validator)
    {
        validator.RuleFor(c => c.Title)
     .NotEmpty()
     .NotNull()
     .WithMessage("Please enter a valid Title");
       


        return validator;
    }
}

