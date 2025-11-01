using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Flights;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.Flights.Queries
{
    public record GetFlightsListingQuery : IRequest<OperationResult<FlightListingResponseDto>>
    {
        public string Departure_Airport { get; set; }
        public string Arrival_Airport { get; set; }
        public string Travel_Date { get; set; }
        public string? Return_Date { get; set; }
        public int ADT { get; set; }
        public int CNN { get; set; }
        public int INF { get; set; }
        public string Class { get; set; } = "Economy";
    }

}
