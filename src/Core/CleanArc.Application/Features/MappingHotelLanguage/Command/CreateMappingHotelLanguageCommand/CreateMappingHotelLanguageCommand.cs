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
using System.Text.Json.Serialization; using CleanArc.Domain.Common;

namespace CleanArc.Application.Features.MappingHotelLanguage.Command.CreateMappingHotelLanguageCommand;

public record CreateMappingHotelLanguageCommand(string? HotelIDs, string? LanguageIDs, int? CreatedBy) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreateMappingHotelLanguageCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateMappingHotelLanguageCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateMappingHotelLanguageCommand> validator)
    {
        validator.RuleFor(c => c.HotelIDs)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid HotelIDs");
        validator.RuleFor(c => c.LanguageIDs)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a LanguageIDs");
        return validator;
    }
}
