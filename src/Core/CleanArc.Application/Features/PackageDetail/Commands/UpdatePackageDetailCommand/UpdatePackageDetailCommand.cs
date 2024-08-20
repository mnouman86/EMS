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
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.PackageDetail.Commands.UpdatePackageDetailCommand;
public record UpdatePackageDetailCommand(int ID, int? PackageTypeID, int? PaymentOrderID, decimal? Stay, decimal? Car,
    decimal? Flight,
    decimal? ThingsToDo,
    int? CultureId,
     int? UpdatedBy) : IRequest<OperationResult<bool>>,
    IValidatableModel<UpdatePackageDetailCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdatePackageDetailCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdatePackageDetailCommand> validator)
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

