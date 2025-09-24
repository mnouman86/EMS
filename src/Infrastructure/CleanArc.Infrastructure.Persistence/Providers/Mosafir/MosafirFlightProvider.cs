using CleanArc.Application.Common;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Flights;
using CleanArc.Domain.Entities.SearchAutoComplete;
using CleanArc.Infrastructure.Persistence.Configuration.FlightsConfig;
using CleanArc.Infrastructure.Persistence.HttpModels;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Persistence.Providers.Mosafir
{
    public class MosafirFlightProvider : IFlightProvider
    {
        private readonly HttpClient _client;
        private readonly MosafirOptions _options;
        private readonly ILogger<MosafirFlightProvider> _logger;
        private readonly ILookupService _lookup;

        public MosafirFlightProvider(HttpClient client, IOptions<MosafirOptions> options, ILogger<MosafirFlightProvider> logger, ILookupService lookup)
        {
            _client = client;
            _options = options.Value;
            _logger = logger;
            _lookup = lookup;
        }

        public async Task<SingleResponseWrapper<FlightSearchResultDto>> SearchFlightsAsync(FlightSearchRequestDto request, CancellationToken cancellationToken = default)
        {
            var mosafirReq = new MosafirRequest
            {
                RawInputStream = new RawInputStream
                {
                    Legs = request.Legs.Select(l => new MosafirLeg
                    {
                        DepartureCode = l.DepartureCode,
                        ArrivalCode = l.ArrivalCode,
                        OutboundDate = l.OutboundDate
                    }).ToList(),
                    AdultsCount = request.AdultsCount,
                    ChildrenCount = request.ChildrenCount,
                    InfantsCount = request.InfantsCount,
                    Cabin = request.Cabin,
                    CurrencyCode = request.CurrencyCode,
                    Locale = request.Locale,
                    DestinationCityName = request.DestinationCityName,
                    FlightTrip = request.FlightTrip
                }
            };

            var url = "API/CallBot/flights_listing"; // relative to BaseUrl

            try
            {
                var resp = await _client.PostAsJsonAsync(url, mosafirReq, cancellationToken);
                if (!resp.IsSuccessStatusCode)
                {
                    var txt = await resp.Content.ReadAsStringAsync(cancellationToken);
                    _logger.LogWarning("Mosafir returned {Status} - {Body}", resp.StatusCode, txt);
                    resp.EnsureSuccessStatusCode();
                }

                var mosafirResp = await resp.Content.ReadFromJsonAsync<MosafirResponse>(cancellationToken: cancellationToken);
                var result = await MapToResultAsync(mosafirResp, cancellationToken);

                var response = new SingleResponseWrapper<FlightSearchResultDto>
                {
                    Data = result,
                    Code = 200,
                    Message = "Data fetched successfully."
                };

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling Mosafir API");
                throw;
            }
        }

        // Async mapping to allow awaiting GetAirlineInfoAsync
        private async Task<FlightSearchResultDto> MapToResultAsync(MosafirResponse? m, CancellationToken ct)
        {
            var result = new FlightSearchResultDto();
            if (m?.FlightItineraries == null) return result;

            // 1) Collect unique airline codes from response to minimize lookups
            var airlineCodes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var itin in m.FlightItineraries)
            {
                if (itin?.Leg1?.Segments == null) continue;
                foreach (var seg in itin.Leg1.Segments)
                {
                    var code = seg?.AirlineCode ?? seg?.OperatingAirlineCode;
                    if (!string.IsNullOrWhiteSpace(code))
                        airlineCodes.Add(code.Trim().ToUpperInvariant());
                }
            }

            // 2) Fire lookups concurrently for unique codes
            var lookupTasks = airlineCodes
                .ToDictionary(code => code, code => _lookup.GetAirlineInfoAsync(code, ct));

            await Task.WhenAll(lookupTasks.Values);

            // Build dictionary of results (some lookups may be null)
            var airlineInfoDict = new Dictionary<string, AirlineInfoDto?>(StringComparer.OrdinalIgnoreCase);
            foreach (var kv in lookupTasks)
            {
                var info = await kv.Value; // already awaited above, this will be completed
                airlineInfoDict[kv.Key] = info;
            }

            // 3) Map itineraries & segments, using preloaded dictionary
            foreach (var it in m.FlightItineraries)
            {
                var dto = new FlightItineraryDto
                {
                    DeeplinkUrl = it.DeeplinkUrl ?? string.Empty,
                    Price = new PriceDto
                    {
                        CurrencyCode = it.Price?.CurrencyCode ?? "PKR",
                        TotalAmount = it.Price?.TotalAmount ?? 0,
                        PricePerAdult = it.Price?.PricePerAdult ?? 0,
                        PricePerChild = it.Price?.PricePerChild ?? 0,
                        PricePerInfant = it.Price?.PricePerInfant ?? 0,
                        IsRefundable = string.Equals(it.Price?.IsRefundable, "true", StringComparison.OrdinalIgnoreCase)
                    },
                    Segments = new List<FlightSegmentDto>()
                };

                if (it.Leg1?.Segments != null)
                {
                    foreach (var s in it.Leg1.Segments)
                    {
                        var segDto = new FlightSegmentDto
                        {
                            Cabin = s.Cabin ?? string.Empty,
                            FlightNumber = s.FlightNumber ?? string.Empty,
                            AirlineCode = s.AirlineCode ?? string.Empty,
                            OperatingAirlineCode = s.OperatingAirlineCode ?? string.Empty,
                            AircraftCode = s.AircraftCode ?? string.Empty,
                            DepartureDateTime = ParseOffset(s.DepartureDateTime),
                            ArrivalDateTime = ParseOffset(s.ArrivalDateTime),
                            DepartureAirportCode = s.DepartureAirportCode ?? string.Empty,
                            ArrivalAirportCode = s.ArrivalAirportCode ?? string.Empty
                        };

                        var code = (!string.IsNullOrWhiteSpace(segDto.AirlineCode) ? segDto.AirlineCode : segDto.OperatingAirlineCode)?.Trim().ToUpperInvariant();
                        if (!string.IsNullOrWhiteSpace(code) && airlineInfoDict.TryGetValue(code, out var info) && info != null)
                        {
                            segDto.AirlineName = info.Name;
                            segDto.AirlineLogo = info.LogoUrl;
                        }

                        dto.Segments.Add(segDto);
                    }
                }

                result.FlightItineraries.Add(dto);
            }

            return result;
        }

        private static DateTimeOffset ParseOffset(string? s)
        {
            if (string.IsNullOrWhiteSpace(s)) return DateTimeOffset.MinValue;
            if (DateTimeOffset.TryParse(s, out var dto)) return dto;
            return DateTimeOffset.MinValue;
        }
    }
}
