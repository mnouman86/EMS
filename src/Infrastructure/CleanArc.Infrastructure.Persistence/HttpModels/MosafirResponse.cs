using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Persistence.HttpModels
{
    public class MosafirResponse
    {
        [JsonPropertyName("flightItineraries")]
        public List<MosafirItinerary>? FlightItineraries { get; set; }
    }

    public class MosafirItinerary
    {
        [JsonPropertyName("leg1")]
        public MosafirLeg1? Leg1 { get; set; }

        [JsonPropertyName("price")]
        public MosafirPrice? Price { get; set; }

        [JsonPropertyName("deeplinkUrl")]
        public string? DeeplinkUrl { get; set; }
    }

    public class MosafirLeg1
    {
        [JsonPropertyName("segments")]
        public List<MosafirSegment>? Segments { get; set; }
    }

    public class MosafirSegment
    {
        public string? Cabin { get; set; }
        public string? FlightNumber { get; set; }
        public string? AirlineCode { get; set; }
        public string? OperatingAirlineCode { get; set; }
        public string? AircraftCode { get; set; }
        public string? DepartureDateTime { get; set; }
        public string? ArrivalDateTime { get; set; }
        public string? DepartureAirportCode { get; set; }
        public string? ArrivalAirportCode { get; set; }
    }

    public class MosafirPrice
    {
        public string? CurrencyCode { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? PricePerAdult { get; set; }
        public decimal? PricePerChild { get; set; }
        public decimal? PricePerInfant { get; set; }
        public string? IsRefundable { get; set; } // "true" / "false"
    }
}
