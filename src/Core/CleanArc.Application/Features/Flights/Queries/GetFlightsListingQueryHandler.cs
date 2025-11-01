using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.SearchAutoComplete.Queries.GetSearchAutoComplete;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Flights;
using CleanArc.SharedKernel.Extensions;
using Mediator;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.Flights.Queries
{
    public class GetFlightsListingQueryHandler : IRequestHandler<GetFlightsListingQuery, OperationResult<FlightListingResponseDto>>
    {
        private readonly IFlightProvider _flightProvider;
        private readonly ILogger<GetFlightsListingQueryHandler> _logger;

        public GetFlightsListingQueryHandler(IFlightProvider flightProvider, ILogger<GetFlightsListingQueryHandler> logger)
        {
            _flightProvider = flightProvider;
            _logger = logger;
        }

        public async ValueTask<OperationResult<FlightListingResponseDto>> Handle(GetFlightsListingQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Searching flights - ", request);
            var result = await _flightProvider.SearchFlightsLitingAsync(request, cancellationToken);
            return OperationResult<FlightListingResponseDto>.SuccessResult(
                result.Data,
                result.Code,
                result.Message
            );
        }
    }
}
