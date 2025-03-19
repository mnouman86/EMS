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

namespace CleanArc.Application.Features.PopularItemsVisit.Commands.CreatePopularItemsVisitCommand;
public record CreatePopularItemsVisitCommand(int? UserID, string? PageVisiteUrl, DateTime? FirstVisitAt,
    DateTime? LastVisitAt, int? VisitCount, string? SessionDuration,int? CreatedBy, int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreatePopularItemsVisitCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreatePopularItemsVisitCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreatePopularItemsVisitCommand> validator)
    {
       
        validator.RuleFor(c => c.UserID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid UserID");
        validator.RuleFor(c => c.PageVisiteUrl)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid PageVisiteUrl");
        validator.RuleFor(c => c.VisitCount)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid VisitCount");
        validator.RuleFor(c => c.FirstVisitAt)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid FirstVisitAt");
        validator.RuleFor(c => c.LastVisitAt)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid LastVisitAt");
        validator.RuleFor(c => c.SessionDuration)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid SessionDuration");

        return validator;
    }
}

