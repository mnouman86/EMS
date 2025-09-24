using CleanArc.Application.Common;
using CleanArc.Application.Models.Flights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence
{
    public interface IFlightProvider
    {
        Task<SingleResponseWrapper<FlightSearchResultDto>> SearchFlightsAsync(FlightSearchRequestDto request, CancellationToken cancellationToken = default);
    }
}
