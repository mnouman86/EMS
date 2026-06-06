using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.Student.Command.BulkImportStudentsCommand
{
    public record StudentImportRowDto(
        int? SrNo,
        string? StudentId,
        string? FormNo,
        string? StudentName,
        string? FatherName,
        string? EmergencyContact,
        string? ClassName);

    public record BulkImportStudentsCommand(
        List<StudentImportRowDto> Rows,
        bool UpdateExisting,
        int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
        IValidatableModel<BulkImportStudentsCommand>
    {
        [JsonIgnore]
        public int UserId { get; set; }

        public IValidator<BulkImportStudentsCommand> ValidateApplicationModel(
            ApplicationBaseValidationModelProvider<BulkImportStudentsCommand> validator)
        {
            validator.RuleFor(c => c.Rows).NotNull().NotEmpty().WithMessage("No rows to import");
            return validator;
        }
    }
}
