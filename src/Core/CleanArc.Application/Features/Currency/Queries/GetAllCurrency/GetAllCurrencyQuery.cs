using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.Currency.Queries.GetAllCurrency;

public record GetAllCurrencyQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllCurrencyQueryResult>>>;

