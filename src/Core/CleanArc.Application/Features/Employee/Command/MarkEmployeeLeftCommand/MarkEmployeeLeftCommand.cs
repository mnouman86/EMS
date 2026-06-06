using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.Employee.Command.MarkEmployeeLeftCommand
{
    public record MarkEmployeeLeftCommand(
        int Id,
        DateTime? LastWorkingDay,
        string? ReasonForLeaving,
        int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
        IValidatableModel<MarkEmployeeLeftCommand>
    {
        [JsonIgnore]
        public int UserId { get; set; }

        public IValidator<MarkEmployeeLeftCommand> ValidateApplicationModel(
            ApplicationBaseValidationModelProvider<MarkEmployeeLeftCommand> validator)
        {
            validator.RuleFor(c => c.Id).GreaterThan(0).WithMessage("Invalid Employee Id");
            validator.RuleFor(c => c.LastWorkingDay).NotNull().WithMessage("Last Working Day is required");
            return validator;
        }
    }
}
