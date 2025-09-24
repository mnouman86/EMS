using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.Flights
{
    public class FlightSearchResultDto
    {
        public List<FlightItineraryDto> FlightItineraries { get; set; } = new();
    }

    public class FlightItineraryDto
    {
        public List<FlightSegmentDto> Segments { get; set; } = new();
        public PriceDto Price { get; set; } = new();
        public string DeeplinkUrl { get; set; } = string.Empty;
    }

    public class FlightSegmentDto
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
        public string? AirlineName { get; set; }
        public string? AirlineLogo { get; set; } // relative url like /airlinelogos/PA.png
    }

    public class PriceDto
    {
        public string CurrencyCode { get; set; } = default!;
        public decimal TotalAmount { get; set; }
        public decimal PricePerAdult { get; set; }
        public decimal PricePerChild { get; set; }
        public decimal PricePerInfant { get; set; }
        public bool IsRefundable { get; set; }
    }
}
