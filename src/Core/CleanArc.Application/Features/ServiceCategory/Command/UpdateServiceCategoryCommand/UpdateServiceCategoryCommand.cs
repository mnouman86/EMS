
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

namespace CleanArc.Application.Features.ServiceCategory.Command.UpdateServiceCategoryCommand;

public record  UpdateServiceCategoryCommand(int Id, string? Name, string? Description, int? ServiceId,string? Icon, int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
IValidatableModel<UpdateServiceCategoryCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateServiceCategoryCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateServiceCategoryCommand> validator)
    {
        validator.RuleFor(c => c.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid Name");
        //validator.RuleFor(c => c.Description)
        //    .NotEmpty()
        //    .NotNull()
        //    .WithMessage("Please enter Description");
        validator.RuleFor(c => c.ServiceId)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please select a Service");
        //validator.RuleFor(c => c.Icon)
        //    .NotEmpty()
        //    .NotNull()
        //    .WithMessage("Please enter a valid Google Icon code from https://fonts.google.com/icons");
        return validator;
    }
}

