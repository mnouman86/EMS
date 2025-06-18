using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.Currency.Queries.GetAllCurrency;

public record GetCurrencyRatesQuery(String baseCurrency = "PKR") : IRequest<OperationResult<List<GetCurrencyRatesQueryResult>>>;

