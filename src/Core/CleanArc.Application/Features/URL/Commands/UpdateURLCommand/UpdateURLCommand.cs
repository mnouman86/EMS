using CleanArc.Application.Models.Request;using System.Text.Json.Serialization; using CleanArc.Domain.Common;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;

namespace CleanArc.Application.Features.URL.Commands.UpdateURLCommand;

public record UpdateURLCommand(int Id,string Path, string Title, string Description) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<UpdateURLCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }

    public IValidator<UpdateURLCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateURLCommand> validator)
    {
        validator.RuleFor(c => c.Path)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid path / URL");
        validator.RuleFor(c => c.Title)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a title");
        return validator;
    }
}