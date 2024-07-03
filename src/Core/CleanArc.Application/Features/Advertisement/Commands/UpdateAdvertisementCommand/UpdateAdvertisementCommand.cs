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

namespace CleanArc.Application.Features.Advertisement.Commands.UpdateAdvertisementCommand;
public record UpdateAdvertisementCommand(int ID, int? PageID, int? PlaceID, string? ImageTitle, string? ImagePath, string? Url, DateTime? StartDate, DateTime? EndDate, int? UpdatedBy) : IRequest<OperationResult<bool>>,
    IValidatableModel<UpdateAdvertisementCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateAdvertisementCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateAdvertisementCommand> validator)
    {
        validator.RuleFor(c => c.PageID)
     .NotEmpty()
     .NotNull()
     .WithMessage("Please enter a valid PageID");
        validator.RuleFor(c => c.PlaceID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a PlaceID");
        validator.RuleFor(c => c.ImageTitle)
    .NotEmpty()
    .NotNull()
    .WithMessage("Please enter a ImageTitle");
        validator.RuleFor(c => c.ImagePath)
    .NotEmpty()
    .NotNull()
    .WithMessage("Please enter a ImagePath");
        validator.RuleFor(c => c.Url)
    .NotEmpty()
    .NotNull()
    .WithMessage("Please enter a Url");
        validator.RuleFor(c => c.StartDate)
    .NotEmpty()
    .NotNull()
    .WithMessage("Please enter a StartDate");
        validator.RuleFor(c => c.EndDate)
.NotEmpty()
.NotNull()
.WithMessage("Please enter a EndDate");
        return validator;
    }
}

