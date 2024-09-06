using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.KBCAttraction.Queries.GetAllKBCAttraction;

public record GetAllKBCAttractionQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllKBCAttractionQueryResult>>>;

