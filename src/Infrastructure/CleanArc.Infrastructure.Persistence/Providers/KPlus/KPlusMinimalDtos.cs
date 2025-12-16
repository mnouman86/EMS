using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Persistence.Providers.KPlus
{
    public class KPlusCreateTokenRequest
    {
        [JsonPropertyName("channelCredential")]
        public KPlusChannelCredential ChannelCredential { get; set; } = new KPlusChannelCredential();
    }

    public class KPlusChannelCredential
    {
        [JsonPropertyName("ChannelCode")]
        public string ChannelCode { get; set; } = string.Empty;

        [JsonPropertyName("ChannelPassword")]
        public string ChannelPassword { get; set; } = string.Empty;
    }

    // Token response (minimal)
    public class KPlusCreateTokenResponse
    {
        [JsonPropertyName("HasError")]
        public bool HasError { get; set; }

        [JsonPropertyName("Result")]
        public KPlusTokenResult? Result { get; set; }

        [JsonPropertyName("ErrorMessage")]
        public string? ErrorMessage { get; set; }
    }

    public class KPlusTokenResult
    {
        [JsonPropertyName("TokenCode")]
        public string? TokenCode { get; set; }

        [JsonPropertyName("ExpiresAt")]
        public string? ExpiresAt { get; set; }
    }

    // Minimal Search Request
    public class KPlusSearchRequestWrapper
    {
        [JsonPropertyName("request")]
        public KPlusSearchRequest Request { get; set; } = new KPlusSearchRequest();
    }

    public class KPlusSearchRequest
    {
        [JsonPropertyName("Legs")]
        public List<KPlusLeg> Legs { get; set; } = new List<KPlusLeg>();

        [JsonPropertyName("Passengers")]
        public List<KPlusPassenger> Passengers { get; set; } = new List<KPlusPassenger>();

        [JsonPropertyName("SearchType")]
        public string SearchType { get; set; } = "1";

        [JsonPropertyName("Token")]
        public KPlusTokenReference Token { get; set; } = new KPlusTokenReference();

        [JsonPropertyName("AdvancedOptions")]
        public KPlusAdvancedOptions? AdvancedOptions { get; set; }
    }

    public class KPlusLeg
    {
        [JsonPropertyName("DeparturePoint")]
        public KPlusPoint DeparturePoint { get; set; } = new KPlusPoint();

        [JsonPropertyName("ArrivalPoint")]
        public KPlusPoint ArrivalPoint { get; set; } = new KPlusPoint();

        [JsonPropertyName("Date")]
        public string Date { get; set; } = string.Empty; // "dd.MM.yyyy" or "yyyy-MM-dd" depending on API
    }

    public class KPlusPoint
    {
        [JsonPropertyName("Code")]
        public string Code { get; set; } = string.Empty;

        [JsonPropertyName("HotpointType")]
        public string HotpointType { get; set; } = "1";
    }

    public class KPlusPassenger
    {
        [JsonPropertyName("Count")]
        public string Count { get; set; } = "1";

        [JsonPropertyName("PaxType")]
        public string PaxType { get; set; } = "0"; // 0: adult, 1: child, 2:infant
    }

    public class KPlusTokenReference
    {
        [JsonPropertyName("TokenCode")]
        public string TokenCode { get; set; } = string.Empty;
    }

    public class KPlusAdvancedOptions
    {
        [JsonPropertyName("Air")]
        public KPlusAirAdvanced? Air { get; set; }
    }

    public class KPlusAirAdvanced
    {
        [JsonPropertyName("OnlyBestFares")]
        public bool OnlyBestFares { get; set; } = false;

        [JsonPropertyName("OnlyDirectFlights")]
        public bool OnlyDirectFlights { get; set; } = false;

        [JsonPropertyName("OnlyRefundableFlights")]
        public bool OnlyRefundableFlights { get; set; } = false;

        [JsonPropertyName("PermittedAirlineCodes")]
        public List<string>? PermittedAirlineCodes { get; set; }
    }

    // Minimal Search Response (only the pieces we need for mapping)
    public class KPlusSearchResponse
    {
        [JsonPropertyName("HasError")]
        public bool HasError { get; set; }

        [JsonPropertyName("Result")]
        public KPlusSearchResult? Result { get; set; }

        [JsonPropertyName("ErrorMessage")]
        public string? ErrorMessage { get; set; }
    }

    public class KPlusSearchResult
    {
        [JsonPropertyName("SearchResults")]
        public List<KPlusSearchResultContainer>? SearchResults { get; set; }
    }

    public class KPlusSearchResultContainer
    {
        [JsonPropertyName("Results")]
        public List<KPlusResultItem>? Results { get; set; }
    }

    public class KPlusResultItem
    {
        [JsonPropertyName("Fares")]
        public List<KPlusFare>? Fares { get; set; }

        [JsonPropertyName("Legs")]
        public List<KPlusLegGroup>? Legs { get; set; }
    }

    public class KPlusFare
    {
        [JsonPropertyName("TotalPrice")]
        public KPlusMoney? TotalPrice { get; set; }

        [JsonPropertyName("PassengerFares")]
        public List<KPlusPassengerFare>? PassengerFares { get; set; }
    }

    public class KPlusPassengerFare
    {
        [JsonPropertyName("PassengerType")]
        public int PassengerType { get; set; }

        [JsonPropertyName("Count")]
        public int Count { get; set; }

        [JsonPropertyName("TotalPrice")]
        public KPlusMoney? TotalPrice { get; set; }
    }

    public class KPlusMoney
    {
        [JsonPropertyName("CurrencyCode")]
        public string? CurrencyCode { get; set; }

        [JsonPropertyName("TotalAmount")]
        public decimal TotalAmount { get; set; }
    }

    public class KPlusLegGroup
    {
        [JsonPropertyName("SearchLeg")]
        public KPlusLeg? SearchLeg { get; set; }

        [JsonPropertyName("AlternativeLegs")]
        public List<KPlusAlternativeLeg>? AlternativeLegs { get; set; }
    }

    public class KPlusAlternativeLeg
    {
        [JsonPropertyName("FareSegments")]
        public List<KPlusFareSegment>? FareSegments { get; set; }
    }

    public class KPlusFareSegment
    {
        [JsonPropertyName("FlightNo")]
        public string? FlightNo { get; set; }

        [JsonPropertyName("DepartureDate")]
        public string? DepartureDate { get; set; }

        [JsonPropertyName("ArrivalDate")]
        public string? ArrivalDate { get; set; }

        [JsonPropertyName("DepartureAirport")]
        public KPlusAirport? DepartureAirport { get; set; }

        [JsonPropertyName("ArrivalAirport")]
        public KPlusAirport? ArrivalAirport { get; set; }

        [JsonPropertyName("FlightDuration")]
        public int FlightDuration { get; set; } // minutes maybe
    }

    public class KPlusAirport
    {
        [JsonPropertyName("Code")]
        public string? Code { get; set; }
    }
}
