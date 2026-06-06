using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.AcademicYear.Command.CreateAcademicYearCommand
{
    public record CreateAcademicYearCommand(string? Code, string? DisplayName, DateTime StartDate, DateTime EndDate, bool IsOpen)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<CreateAcademicYearCommand>
    {
        [JsonIgnore] public int UserId { get; set; }
        public IValidator<CreateAcademicYearCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateAcademicYearCommand> v)
        {
            v.RuleFor(c => c.Code).NotEmpty().MaximumLength(20);
            v.RuleFor(c => c.DisplayName).NotEmpty().MaximumLength(100);
            v.RuleFor(c => c).Must(c => c.EndDate > c.StartDate).WithMessage("EndDate must be after StartDate");
            return v;
        }
    }
}
