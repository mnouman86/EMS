using CleanArc.Application.Features.AgeType.Commands.CreateAgeTypeCommand;
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

namespace CleanArc.Application.Features.CustomerReview.Command.CreateCustomerReviewCommand;

public  record CreateCustomerReviewCommand(int? GenericTitleID,  int? ServiceCategoryID,
int? Rating,
string? Description,
string? Status,
int? ApprovedBy,
int? CultureId,
//DateTime? ApprovedDate,
int? CreatedBy) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreateCustomerReviewCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateCustomerReviewCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateCustomerReviewCommand> validator)
    {
        validator.RuleFor(c => c.GenericTitleID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid GenericTitleID");
        validator.RuleFor(c => c.ServiceCategoryID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a ServiceCategoryID");
        validator.RuleFor(c => c.Rating)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a valid Rating");
      
        return validator;
    }
}

