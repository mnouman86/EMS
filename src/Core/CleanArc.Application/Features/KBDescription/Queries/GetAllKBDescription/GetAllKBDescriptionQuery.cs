using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.KBDescription.Queries.GetAllKBDescription;

public record GetAllKBDescriptionQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllKBDescriptionQueryResult>>>;

