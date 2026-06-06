using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.Result.Command.EnterMarksCommand
{
    public record MarksRow(
        int StudentId,
        decimal? Written,
        decimal? Oral,
        decimal? AttrPunctuality,
        decimal? AttrDiscipline,
        decimal? AttrClassParticipation,
        decimal? AttrCreativity,
        decimal? AttrBehaviorWithPeers);

    public record EnterMarksCommand(
        int ResultSessionId,
        int SchoolClassId,
        int SubjectId,
        int TeacherEmployeeId,
        List<MarksRow> Entries)
        : IRequest<OperationResult<ResponseEntity>>,
          IValidatableModel<EnterMarksCommand>
    {
        [JsonIgnore]
        public int UserId { get; set; }

        public IValidator<EnterMarksCommand> ValidateApplicationModel(
            ApplicationBaseValidationModelProvider<EnterMarksCommand> validator)
        {
            validator.RuleFor(c => c.ResultSessionId).GreaterThan(0);
            validator.RuleFor(c => c.SchoolClassId).GreaterThan(0);
            validator.RuleFor(c => c.SubjectId).GreaterThan(0);
            validator.RuleFor(c => c.TeacherEmployeeId).GreaterThan(0).WithMessage("TeacherEmployeeId is required");
            validator.RuleFor(c => c.Entries).NotNull().NotEmpty().WithMessage("At least one student row is required");
            return validator;
        }
    }
}
