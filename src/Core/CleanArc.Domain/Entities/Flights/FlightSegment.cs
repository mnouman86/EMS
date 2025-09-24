using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.Flights
{
    public class FlightSegment
    {
        public string Cabin { get; set; } = default!;
        public string FlightNumber { get; set; } = default!;
        public string AirlineCode { get; set; } = default!;
        public string OperatingAirlineCode { get; set; } = default!;
        public string AircraftCode { get; set; } = default!;
        public DateTimeOffset DepartureDateTime { get; set; }
        public DateTimeOffset ArrivalDateTime { get; set; }
        public string DepartureAirportCode { get; set; } = default!;
        public string ArrivalAirportCode { get; set; } = default!;
    }
}
