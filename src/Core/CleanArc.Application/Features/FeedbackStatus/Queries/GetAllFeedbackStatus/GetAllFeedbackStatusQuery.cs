using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.FeedbackStatus.Queries.GetAllFeedbackStatus;

public record GetAllFeedbackStatusQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllFeedbackStatusQueryResult>>>;

