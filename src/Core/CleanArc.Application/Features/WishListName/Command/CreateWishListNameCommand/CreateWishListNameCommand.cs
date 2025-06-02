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

namespace CleanArc.Application.Features.WishListName.Command.CreateWishListNameCommand
{
    public record CreateWishListNameCommand(string? Name, string? Description, bool? IsDefault, int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreateWishListNameCommand>
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public IValidator<CreateWishListNameCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateWishListNameCommand> validator)
        {
            validator.RuleFor(c => c.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Please enter a valid Name");
            return validator;
        }
    }


}
