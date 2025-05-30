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

namespace CleanArc.Application.Features.WishList.Command.CreateWishListCommand
{
    public record CreateWishListCommand(int EnumServiceTypeId, int GenericTitleId,
    int WishListNameLookUpId, int? Priority, int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreateWishListCommand>
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public IValidator<CreateWishListCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateWishListCommand> validator)
        {
            validator.RuleFor(c => c.EnumServiceTypeId)
                .NotEmpty()
                .NotNull()
                .WithMessage("Please enter a valid Enum Service Type Id");
            return validator;
        }
    }


}
