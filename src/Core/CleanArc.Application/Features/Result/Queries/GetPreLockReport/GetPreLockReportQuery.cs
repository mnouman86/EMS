using CleanArc.Application.Models.Common;
using Mediator;

namespace CleanArc.Application.Features.Result.Queries.GetPreLockReport;

public record GetPreLockReportQuery(int ResultSessionId, int SchoolClassId)
    : IRequest<OperationResult<List<PreLockMissingRow>>>;

public class PreLockMissingRow
{
    public int StudentId { get; set; }
    public string StudentCode { get; set; }
    public string StudentFullName { get; set; }
    public int SubjectId { get; set; }
    public string SubjectName { get; set; }
    public string Missing { get; set; }
}
