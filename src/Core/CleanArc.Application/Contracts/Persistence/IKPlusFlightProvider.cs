using CleanArc.Application.Common;
using CleanArc.Application.Features.Flights.Queries;
using CleanArc.Application.Models.Flights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence
{
    public interface IKPlusFlightProvider
    {
        Task<SingleResponseWrapper<CleanArc.Application.Models.Flights.FlightListingResponseDto>> SearchFlightsLitingAsync(GetFlightsListingQuery query, CancellationToken cancellationToken = default);
    }
}
