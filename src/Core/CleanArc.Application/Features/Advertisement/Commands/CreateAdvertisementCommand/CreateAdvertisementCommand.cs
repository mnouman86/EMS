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

namespace CleanArc.Application.Features.Advertisement.Commands.CreateAdvertisementCommand;
public record CreateAdvertisementCommand(int? PageId, int? PlaceId, string? ImageTitle, List<string>? ImagePaths, string? Url, DateTime? StartDate, DateTime? EndDate, bool? IsShow, int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreateAdvertisementCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateAdvertisementCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateAdvertisementCommand> validator)
    {
        validator.RuleFor(c => c.PageId)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid PageID");
        validator.RuleFor(c => c.PlaceId)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a PlaceID");
        validator.RuleFor(c => c.ImageTitle)
    .NotEmpty()
    .NotNull()
    .WithMessage("Please enter a ImageTitle");
                validator.RuleFor(c => c.ImagePaths)
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

