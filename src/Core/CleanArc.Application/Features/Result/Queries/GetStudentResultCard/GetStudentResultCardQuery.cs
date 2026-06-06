using CleanArc.Application.Features.Result.Queries.GetMarksEntryGrid;
using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;

namespace CleanArc.Application.Features.Result.Queries.GetStudentResultCard;

public record GetStudentResultCardQuery(int ResultSessionId, int StudentId)
    : IRequest<OperationResult<GetStudentResultCardQueryResult>>;

public class GetStudentResultCardQueryResult
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
    public DateTime IssuedAt { get; set; }
    public string FileNameSuggestion { get; set; }
    public List<MarksEntryGridRow> Marks { get; set; } = new();
}
