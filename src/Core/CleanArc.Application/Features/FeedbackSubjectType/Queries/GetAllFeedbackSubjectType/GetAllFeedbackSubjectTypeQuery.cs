using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.FeedbackSubjectType.Queries.GetAllFeedbackSubjectType;

public record GetAllFeedbackSubjectTypeQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllFeedbackSubjectTypeQueryResult>>>;

