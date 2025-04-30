using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.Activity.Queries.GetAllSearchActivityDetail;

public record GetAllSearchActivityDetailQuery(ActivitySearchRequest searchRequest) : IRequest<OperationResult<GetAllSearchActivityDetailQueryResult>>;

