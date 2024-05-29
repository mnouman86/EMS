using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;

namespace CleanArc.Application.Features.URL.Queries.GetAllURLs;
public record GetAllURLsQuery(SearchRequest searchRequest) :IRequest<OperationResult<List<GetAllURLsQueryResult>>>;