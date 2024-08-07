using System.Text.Json.Serialization;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;

namespace CleanArc.Application.Features.URL.Commands.DeleteURLCommand;

//public record DeleteURLCommand(int Id,int UpdatedBy) : IRequest<OperationResult<bool>>,
//    IValidatableModel<DeleteURLCommand>
public record DeleteURLCommand(string SelectedIds,int? CultureId) : IRequest<OperationResult<bool>>,
    IValidatableModel<DeleteURLCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }

    public IValidator<DeleteURLCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<DeleteURLCommand> validator)
    {
        validator.RuleFor(c => c.SelectedIds)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please select Record");
        return validator;
    }
}