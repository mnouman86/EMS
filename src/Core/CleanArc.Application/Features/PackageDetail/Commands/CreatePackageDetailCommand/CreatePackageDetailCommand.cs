using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using CleanArc.SharedKernel.ValidationBase;
using FluentValidation;
using System.Text.Json.Serialization; using CleanArc.Domain.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.PackageDetail.Commands.CreatePackageDetailCommand;
public record CreatePackageDetailCommand(int? PackageTypeID, int? PaymentOrderID, decimal? Stay, decimal? Car,
    decimal? Flight,
    decimal? ThingsToDo,
    int? CultureId,
    int? CreatedBy) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreatePackageDetailCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreatePackageDetailCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreatePackageDetailCommand> validator)
    {
        validator.RuleFor(c => c.PackageTypeID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid PackageTypeID");
        validator.RuleFor(c => c.PaymentOrderID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a PaymentOrderID");
        validator.RuleFor(c => c.Stay)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a Stay");
        validator.RuleFor(c => c.Car)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a Car");
        validator.RuleFor(c => c.Flight)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a Flight");
        validator.RuleFor(c => c.ThingsToDo)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a ThingsToDo");
       
        
        return validator;
    }
}

