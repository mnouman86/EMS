using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Flights;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.Flights.Queries.KPlus
{
    public record KPlusSearchFlightsQuery : IRequest<OperationResult<FlightListingResponseDto>>
    {
        public string Departure_Airport { get; init; } = string.Empty;
        public string Arrival_Airport { get; init; } = string.Empty;
        public string Travel_Date { get; init; } = string.Empty;
        public string? Return_Date { get; init; }
        public int ADT { get; init; }
        public int CNN { get; init; }
        public int INF { get; init; }
        public string Class { get; init; } = "Economy";
    }
}
