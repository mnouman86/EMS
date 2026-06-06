using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Result;
using Mediator;

namespace CleanArc.Application.Features.Result.Queries.GetMarksEntryGrid;

public record GetMarksEntryGridQuery(MarksEntryGridRequest request)
    : IRequest<OperationResult<List<MarksEntryGridRow>>>;

public class MarksEntryGridRow
{
    public int StudentId { get; set; }
    public string StudentCode { get; set; }
    public string StudentFullName { get; set; }
    public int SubjectId { get; set; }
    public string SubjectName { get; set; }
    public bool IsRTL { get; set; }
    public decimal? Written { get; set; }
    public decimal? Oral { get; set; }
    public decimal? AttrPunctuality { get; set; }
    public decimal? AttrDiscipline { get; set; }
    public decimal? AttrClassParticipation { get; set; }
    public decimal? AttrCreativity { get; set; }
    public decimal? AttrBehaviorWithPeers { get; set; }
    public decimal? PerSubjectPercent { get; set; }
    public string Grade { get; set; }
    public bool? IsLocked { get; set; }
}
