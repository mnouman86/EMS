using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Flights;
using Mediator;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.Flights.Queries.KPlus
{
    public class KPlusSearchFlightsQueryHandler : IRequestHandler<KPlusSearchFlightsQuery, OperationResult<FlightListingResponseDto>>
    {
        private readonly IKPlusFlightProvider _provider;
        private readonly ILogger<KPlusSearchFlightsQueryHandler> _logger;

        public KPlusSearchFlightsQueryHandler(IKPlusFlightProvider provider, ILogger<KPlusSearchFlightsQueryHandler> logger)
        {
            _provider = provider;
            _logger = logger;
        }

        public async ValueTask<OperationResult<FlightListingResponseDto>> Handle(KPlusSearchFlightsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("KPlus search - {@request}", request);

            // Reuse same GetFlightsListingQuery-compatible data shape by mapping into provider call
            var providerResult = await _provider.SearchFlightsLitingAsync(new GetFlightsListingQuery
            {
                Departure_Airport = request.Departure_Airport,
                Arrival_Airport = request.Arrival_Airport,
                Travel_Date = request.Travel_Date,
                Return_Date = request.Return_Date,
                ADT = request.ADT,
                CNN = request.CNN,
                INF = request.INF,
                Class = request.Class
            }, cancellationToken);

            if (providerResult?.Data == null)
            {
                return OperationResult<FlightListingResponseDto>.FailureResult("No data from KPlus");
            }

            return OperationResult<FlightListingResponseDto>.SuccessResult(providerResult.Data, providerResult.Code, providerResult.Message);
        }
    }
}
