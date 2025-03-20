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

namespace CleanArc.Application.Features.MappingHotelLanguage.Command.UpdateMappingHotelLanguageCommand
{
    public record UpdateMappingHotelLanguageCommand(int Id, string? Name, string? Description, int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<UpdateMappingHotelLanguageCommand>
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public IValidator<UpdateMappingHotelLanguageCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateMappingHotelLanguageCommand> validator)
        {
            validator.RuleFor(c => c.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Please enter a valid Name");
            return validator;
        }
    }

}
