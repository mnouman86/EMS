using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.Flights
{
    public record LegDto(string DepartureCode, string ArrivalCode, string OutboundDate);

    public class FlightSearchRequestDto
    {
        public List<LegDto> Legs { get; set; } = new();
        public int AdultsCount { get; set; }
        public int ChildrenCount { get; set; }
        public int InfantsCount { get; set; }
        public string Cabin { get; set; } = "Economy";
        public string CurrencyCode { get; set; } = "PKR";
        public string Locale { get; set; } = "en";
        public string DestinationCityName { get; set; } = string.Empty;
        public string FlightTrip { get; set; } = "Single";
    }
}
