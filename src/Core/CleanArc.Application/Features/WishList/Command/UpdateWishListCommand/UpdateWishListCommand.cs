using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CleanArc.Application.Models.Request;using System.Text.Json.Serialization; using CleanArc.Domain.Common;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.WishList.Command.UpdateWishListCommand;

public record UpdateWishListCommand(int Id, int EnumServiceTypeId,int GenericTitleId, 
    int WishListNameLookUpId, int? Priority, int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
IValidatableModel<UpdateWishListCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateWishListCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateWishListCommand> validator)
    {
        validator.RuleFor(c => c.EnumServiceTypeId)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid Enum Service Type Id");
        return validator;
    }
}
