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

namespace CleanArc.Application.Features.LastMinuteDeal.Command.CreateLastMinuteDealCommand
{
    public record CreateLastMinuteDealCommand(int ServiceTypeEnumId, int GenericTitleId,
    int FilterCategoryLookUpId, decimal? Discount, DateTime? StartDate, DateTime? EndDate, int? Priority, int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreateLastMinuteDealCommand>
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public IValidator<CreateLastMinuteDealCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateLastMinuteDealCommand> validator)
        {
            validator.RuleFor(c => c.ServiceTypeEnumId)
                .NotEmpty()
                .NotNull()
                .WithMessage("Please enter a valid Enum Service Type Id");
            return validator;
        }
    }


}
