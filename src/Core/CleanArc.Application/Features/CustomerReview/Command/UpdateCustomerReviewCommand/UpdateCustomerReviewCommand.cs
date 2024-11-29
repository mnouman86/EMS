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
using System.Text.Json.Serialization; using CleanArc.Domain.Common;
using Azure.Core;

namespace CleanArc.Application.Features.CustomerReview.Command.UpdateCustomerReviewCommand
{
    public record UpdateCustomerReviewCommand(int ID, int? GenericTitleID, int? ServiceCategoryID,
int? Rating,
string? Description,
string? Status,
int? ApprovedBy, int? CultureId,
    string? ApprovedDate, int? UpdatedBy) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<UpdateCustomerReviewCommand>
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public IValidator<UpdateCustomerReviewCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateCustomerReviewCommand> validator)
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
               .WithMessage("Please enter a valid Rating"); return validator;
        }
    }
}
