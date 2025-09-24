using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Persistence.HttpModels
{
    public class MosafirRequest
    {
        [JsonPropertyName("raw_input_stream")]
        public RawInputStream RawInputStream { get; set; } = new();
    }

    public class RawInputStream
    {
        [JsonPropertyName("legs")]
        public List<MosafirLeg> Legs { get; set; } = new();
        [JsonPropertyName("adultsCount")]
        public int AdultsCount { get; set; }
        [JsonPropertyName("childrenCount")]
        public int ChildrenCount { get; set; }
        [JsonPropertyName("infantsCount")]
        public int InfantsCount { get; set; }
        [JsonPropertyName("cabin")]
        public string Cabin { get; set; } = "Economy";
        [JsonPropertyName("currencyCode")]
        public string CurrencyCode { get; set; } = "PKR";
        [JsonPropertyName("locale")]
        public string Locale { get; set; } = "en";
        [JsonPropertyName("destinationCityName")]
        public string DestinationCityName { get; set; } = "";
        [JsonPropertyName("flightTrip")]
        public string FlightTrip { get; set; } = "Round";
    }

    public class MosafirLeg
    {
        [JsonPropertyName("departureCode")]
        public string DepartureCode { get; set; } = default!;
        [JsonPropertyName("arrivalCode")]
        public string ArrivalCode { get; set; } = default!;
        [JsonPropertyName("outboundDate")]
        public string OutboundDate { get; set; } = default!; // "yyyy-MM-dd"
    }
}
