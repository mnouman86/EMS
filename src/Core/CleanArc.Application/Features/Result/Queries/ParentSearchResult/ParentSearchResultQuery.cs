using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.Result.Queries.ParentSearchResult;

public record ParentSearchResultQuery(string? StudentCode, string? SecondFactor)
    : IRequest<OperationResult<ParentSearchResultQueryResult>>,
      IValidatableModel<ParentSearchResultQuery>
{
    [JsonIgnore]
    public string? ClientIp { get; set; }

    public IValidator<ParentSearchResultQuery> ValidateApplicationModel(
        ApplicationBaseValidationModelProvider<ParentSearchResultQuery> validator)
    {
        validator.RuleFor(c => c.StudentCode).NotEmpty().WithMessage("Student ID is required");
        validator.RuleFor(c => c.SecondFactor).NotEmpty().WithMessage("Date of Birth or PIN is required");
        return validator;
    }
}

public class ParentSearchResultQueryResult
{
    public int StudentId { get; set; }
    public string StudentCode { get; set; }
    public string StudentFullName { get; set; }
    public int ResultSessionId { get; set; }
    public string SessionName { get; set; }
    public string ClassName { get; set; }
    public decimal? OverallPercent { get; set; }
    public string OverallGrade { get; set; }
    public string Remark { get; set; }
    public System.DateTime IssuedAt { get; set; }
}
