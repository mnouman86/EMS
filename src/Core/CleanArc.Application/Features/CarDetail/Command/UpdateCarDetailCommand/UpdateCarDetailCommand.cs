using CleanArc.Application.Features.AgeType.Commands.UpdateAgeTypeCommand;
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

namespace CleanArc.Application.Features.CarDetail.Command.UpdateCarDetailCommand
{
    public record UpdateCarDetailCommand(int ID, int? BusinessID, string? Model, string? Year, String? VehicleIdentificationNumber, string? PlateNumber, int? NoOfSeat, int? RentPrice, bool? IsRefundable, bool? IsCancelation, int? UpdatedBy) : IRequest<OperationResult<bool>>,
    IValidatableModel<UpdateCarDetailCommand>
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public IValidator<UpdateCarDetailCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateCarDetailCommand> validator)
        {
            validator.RuleFor(c => c.BusinessID)
             .NotEmpty()
             .NotNull()
             .WithMessage("Please enter a valid BusinessID");
            validator.RuleFor(c => c.Model)
                .NotEmpty()
                .NotNull()
                .WithMessage("Please enter a Model");
            validator.RuleFor(c => c.Year)
               .NotEmpty()
               .NotNull()
               .WithMessage("Please enter a valid Year");
            validator.RuleFor(c => c.VehicleIdentificationNumber)
                .NotEmpty()
                .NotNull()
                .WithMessage("Please enter a VehicleIdentificationNumber");
            validator.RuleFor(c => c.NoOfSeat)
              .NotEmpty()
              .NotNull()
              .WithMessage("Please enter a valid NoOfSeat");
            validator.RuleFor(c => c.RentPrice)
                .NotEmpty()
                .NotNull()
                .WithMessage("Please enter a RentPrice");
            return validator;
        }
    }
}
