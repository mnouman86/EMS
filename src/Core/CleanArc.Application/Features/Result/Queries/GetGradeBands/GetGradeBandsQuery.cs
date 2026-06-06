using CleanArc.Application.Models.Common;
using Mediator;

namespace CleanArc.Application.Features.Result.Queries.GetGradeBands;

public record GetGradeBandsQuery() : IRequest<OperationResult<List<GetGradeBandsQueryResult>>>;

public class GetGradeBandsQueryResult
{
    public int Id { get; set; }
    public string Grade { get; set; }
    public decimal MinPercent { get; set; }
    public decimal MaxPercent { get; set; }
    public string RemarkTemplate { get; set; }
    public int DisplayOrder { get; set; }
}
