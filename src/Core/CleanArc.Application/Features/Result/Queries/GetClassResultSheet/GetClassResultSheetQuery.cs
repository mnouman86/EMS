using CleanArc.Application.Models.Common;
using Mediator;

namespace CleanArc.Application.Features.Result.Queries.GetClassResultSheet;

public record GetClassResultSheetQuery(int ResultSessionId, int SchoolClassId)
    : IRequest<OperationResult<List<ClassSheetRowResult>>>;

public class ClassSheetRowResult
{
    public int StudentId { get; set; }
    public string StudentCode { get; set; }
    public string StudentFullName { get; set; }
    public decimal? OverallPercent { get; set; }
    public string OverallGrade { get; set; }
    public string Remark { get; set; }
}
