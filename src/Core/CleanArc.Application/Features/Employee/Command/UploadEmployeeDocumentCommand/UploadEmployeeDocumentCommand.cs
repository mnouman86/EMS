using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.Employee.Command.UploadEmployeeDocumentCommand
{
    public record UploadEmployeeDocumentCommand(
        int EmployeeId,
        string? DocumentType,
        string? FileName,
        string? FilePath,
        string? ContentType,
        long FileSizeBytes,
        bool IsPhoto,
        int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
        IValidatableModel<UploadEmployeeDocumentCommand>
    {
        [JsonIgnore]
        public int UserId { get; set; }

        public IValidator<UploadEmployeeDocumentCommand> ValidateApplicationModel(
            ApplicationBaseValidationModelProvider<UploadEmployeeDocumentCommand> validator)
        {
            validator.RuleFor(c => c.EmployeeId).GreaterThan(0).WithMessage("Invalid Employee Id");
            validator.RuleFor(c => c.FileName).NotEmpty().WithMessage("File Name is required");
            validator.RuleFor(c => c.FilePath).NotEmpty().WithMessage("File Path is required");
            validator.RuleFor(c => c.DocumentType).NotEmpty().WithMessage("Document Type is required");
            return validator;
        }
    }
}
