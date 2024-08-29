using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.FAQs.Queries.GetAllFAQs;

public record GetAllFAQsQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllFAQsQueryResult>>>;

