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

namespace CleanArc.Application.Features.PopularItemsVisit.Commands.UpdatePopularItemsVisitCommand;
public record UpdatePopularItemsVisitCommand(int ID, int? UserID, string? PageVisiteUrl,
    string? SessionDuration,
    int? VisitCount,
    DateTime? LastVisitAt,  
    int? UpdatedBy, int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<UpdatePopularItemsVisitCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdatePopularItemsVisitCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdatePopularItemsVisitCommand> validator)
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

