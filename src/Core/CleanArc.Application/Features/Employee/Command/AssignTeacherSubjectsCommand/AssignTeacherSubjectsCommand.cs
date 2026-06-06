using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.Employee.Command.AssignTeacherSubjectsCommand
{
    public record ClassSubjectPair(int SchoolClassId, int SubjectId);

    public record AssignTeacherSubjectsCommand(
        int EmployeeId,
        List<ClassSubjectPair>? Assignments,
        int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
        IValidatableModel<AssignTeacherSubjectsCommand>
    {
        [JsonIgnore]
        public int UserId { get; set; }

        public IValidator<AssignTeacherSubjectsCommand> ValidateApplicationModel(
            ApplicationBaseValidationModelProvider<AssignTeacherSubjectsCommand> validator)
        {
            validator.RuleFor(c => c.EmployeeId).GreaterThan(0).WithMessage("Invalid Employee Id");
            return validator;
        }
    }
}
