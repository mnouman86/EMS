using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.ActivityIncludedOption.Queries.GetAllActivityIncludedOption;

public record GetAllActivityIncludedOptionQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllActivityIncludedOptionQueryResult>>>;

