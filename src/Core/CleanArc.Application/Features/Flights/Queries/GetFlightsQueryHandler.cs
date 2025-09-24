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
    public class GetFlightsQueryHandler : IRequestHandler<GetFlightsQuery, OperationResult<FlightSearchResultDto>>
    {
        private readonly IFlightProvider _flightProvider;
        private readonly ILogger<GetFlightsQueryHandler> _logger;

        public GetFlightsQueryHandler(IFlightProvider flightProvider, ILogger<GetFlightsQueryHandler> logger)
        {
            _flightProvider = flightProvider;
            _logger = logger;
        }

        public async ValueTask<OperationResult<FlightSearchResultDto>> Handle(GetFlightsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Searching flights - {legs} adults:{adults}", request.Request.Legs.Count, request.Request.AdultsCount);
            var result = await _flightProvider.SearchFlightsAsync(request.Request, cancellationToken);
            return OperationResult<FlightSearchResultDto>.SuccessResult(
                result.Data,
                result.Code,
                result.Message
            );
        }
    }
}
