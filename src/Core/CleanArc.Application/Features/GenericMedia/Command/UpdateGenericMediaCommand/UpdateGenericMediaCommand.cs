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
using CleanArc.Application.Models.Request;using System.Text.Json.Serialization; using CleanArc.Domain.Common;

namespace CleanArc.Application.Features.GenericMedia.Command.UpdateGenericMediaCommand
{
    public record UpdateGenericMediaCommand(int Id, int? GenericTitleId, string? ImagePath, string? ImageTitle,
    bool? IsMain, int? ServiceTypeEnumId, int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<UpdateGenericMediaCommand>
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public IValidator<UpdateGenericMediaCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateGenericMediaCommand> validator)
        {
            validator.RuleFor(c => c.GenericTitleId)
             .NotEmpty()
             .NotNull()
             .WithMessage("Please enter a valid BusinessID");
            validator.RuleFor(c => c.ImagePath)
               .NotEmpty()
               .NotNull()
               .WithMessage("Please enter a valid ImagePath");
            validator.RuleFor(c => c.ImageTitle)
                .NotEmpty()
                .NotNull()
                .WithMessage("Please enter a ImageTitle");

            return validator;
        }
    }
}
