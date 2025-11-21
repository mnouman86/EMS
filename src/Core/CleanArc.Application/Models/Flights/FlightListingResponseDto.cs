using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.Flights
{
    public class FlightListingResponseDto
    {
        [JsonPropertyName("allFlights")]
        public List<FlightItemDto>? AllFlights { get; set; }
        public FlightItemDto? Cheapest { get; set; }
        public FlightItemDto? Fastest { get; set; }
        public FlightItemDto? Best { get; set; }
    }

    public class FlightItemDto
    {
        [JsonPropertyName("total_price")]
        public string? Total_Price { get; set; }

        [JsonPropertyName("base_fare")]
        public string? Base_Fare { get; set; }

        [JsonPropertyName("taxes")]
        public string? Taxes { get; set; }

        [JsonPropertyName("adult_base_fare")]
        public string? Adult_Base_Fare { get; set; }

        [JsonPropertyName("adult_taxes")]
        public string? Adult_Taxes { get; set; }

        [JsonPropertyName("child_base_fare")]
        public string? Child_Base_Fare { get; set; }

        [JsonPropertyName("child_taxes")]
        public string? Child_Taxes { get; set; }

        [JsonPropertyName("infant_base_fare")]
        public string? Infant_Base_Fare { get; set; }

        [JsonPropertyName("infant_taxes")]
        public string? Infant_Taxes { get; set; }

        [JsonPropertyName("baggage_type")]
        public string? Baggage_Type { get; set; }

        [JsonPropertyName("baggage_type_detail")]
        public string? Baggage_Type_Detail { get; set; }

        [JsonPropertyName("air_line")]
        public string? Air_Line { get; set; }

        [JsonPropertyName("refundable")]
        public bool? Refundable { get; set; }

        [JsonPropertyName("passengers")]
        public PassengerInfo? Passengers { get; set; }

        [JsonPropertyName("sectors")]
        public List<List<FlightSector>>? Sectors { get; set; }

        [JsonPropertyName("extended_properties")]
        public ExtendedProperties? Extended_Properties { get; set; }

        [JsonPropertyName("seat_charges")]
        public decimal? Seat_Charges { get; set; }

        [JsonPropertyName("seat_charges_details")]
        public List<object>? Seat_Charges_Details { get; set; }

        [JsonPropertyName("seats")]
        public string? Seats { get; set; }

        [JsonPropertyName("prop_request")]
        public string? Prop_Request { get; set; }
    }

    public class PassengerInfo
    {
        [JsonPropertyName("adt")]
        public int? Adt { get; set; }

        [JsonPropertyName("cnn")]
        public int? Cnn { get; set; }

        [JsonPropertyName("inf")]
        public int? Inf { get; set; }
    }

    public class FlightSector
    {
        [JsonPropertyName("stops")]
        public int? Stops { get; set; }

        [JsonPropertyName("journey_time")]
        public string? Journey_Time { get; set; }

        [JsonPropertyName("key")]
        public string? Key { get; set; }

        [JsonPropertyName("origin")]
        public string? Origin { get; set; }

        [JsonPropertyName("destination")]
        public string? Destination { get; set; }

        [JsonPropertyName("origin_air_port")]
        public string? Origin_Air_Port { get; set; }

        [JsonPropertyName("destination_air_port")]
        public string? Destination_Air_Port { get; set; }

        [JsonPropertyName("origin_city")]
        public string? Origin_City { get; set; }

        [JsonPropertyName("destination_city")]
        public string? Destination_City { get; set; }

        [JsonPropertyName("origin_state")]
        public string? Origin_State { get; set; }

        [JsonPropertyName("destination_state")]
        public string? Destination_State { get; set; }

        [JsonPropertyName("origin_country")]
        public string? Origin_Country { get; set; }

        [JsonPropertyName("destination_country")]
        public string? Destination_Country { get; set; }

        [JsonPropertyName("air_line")]
        public string? Air_Line { get; set; }

        [JsonPropertyName("air_line_name")]
        public string? Air_Line_Name { get; set; }

        [JsonPropertyName("class")]
        public string? Class { get; set; }

        [JsonPropertyName("flight_number")]
        public string? Flight_Number { get; set; }

        [JsonPropertyName("travel_date")]
        public DateTime? Travel_Date { get; set; }

        [JsonPropertyName("arrival_date")]
        public DateTime? Arrival_Date { get; set; }

        [JsonPropertyName("duration")]
        public string? Duration { get; set; }

        [JsonPropertyName("stop_over_time")]
        public string? Stop_Over_Time { get; set; }

        [JsonPropertyName("available_seats")]
        public string? Available_Seats { get; set; }

        [JsonPropertyName("fare_rule_key")]
        public FareRuleKey? Fare_Rule_Key { get; set; }

        [JsonPropertyName("departure_category")]
        public string? Departure_Category { get; set; }
    }

    public class FareRuleKey
    {
        [JsonPropertyName("FareInfoRef")]
        public string? FareInfoRef { get; set; }

        [JsonPropertyName("ProviderCode")]
        public string? ProviderCode { get; set; }

        [JsonPropertyName("FareRuleKey")]
        public string? FareRuleKeyValue { get; set; }
    }

    public class ExtendedProperties
    {
        [JsonPropertyName("markups")]
        public List<Markup>? Markups { get; set; }
    }

    public class Markup
    {
        [JsonPropertyName("markedup_amount")]
        public decimal? Markedup_Amount { get; set; }

        [JsonPropertyName("strike_through")]
        public int? Strike_Through { get; set; }
    }
}
