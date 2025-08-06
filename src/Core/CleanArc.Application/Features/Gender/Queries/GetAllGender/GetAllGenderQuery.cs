using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.Gender.Queries.GetAllGender;

public record GetAllGenderQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllGenderQueryResult>>>;

