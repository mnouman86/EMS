using CleanArc.Application.Models.Request;using System.Text.Json.Serialization; using CleanArc.Domain.Common;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;

namespace CleanArc.Application.Features.URL.Commands.AddURLCommand;

/// <summary>
/// Represents a command for adding a new URL.
/// </summary>
/// <seealso cref="Mediator.IRequest{CleanArc.Application.Models.Common.OperationResult{System.Boolean}}" />
/// <seealso cref="Mediator.IBaseRequest" />
/// <seealso cref="Mediator.IMessage" />
/// <seealso cref="CleanArc.SharedKernel.ValidationBase.Contracts.IValidatableModel{CleanArc.Application.Features.URL.Commands.AddURLCommand.AddURLCommand}" />
/// <seealso cref="System.IEquatable{CleanArc.Application.Features.URL.Commands.AddURLCommand.AddURLCommand}" />
public record AddURLCommand(string Path, string Title, string Description) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<AddURLCommand>
{
    /// <summary>
    /// Gets or sets the user ID associated with the command.
    /// </summary>
    /// <value>
    /// The user identifier.
    /// </value>
    [JsonIgnore]
    public int UserId { get; set; }

    /// <summary>
    /// Validates the application model.
    /// </summary>
    /// <param name="validator">The validator.</param>
    /// <returns></returns>
    public IValidator<AddURLCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<AddURLCommand> validator)
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