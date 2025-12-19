using CleanArc.Application.Common;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.Flights.Queries;
using CleanArc.Application.Models.Flights;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Persistence.Providers.KPlus
{
    public class KPlusFlightProvider : IKPlusFlightProvider
    {
        private readonly HttpClient _client;
        private readonly KPlusOptions _options;
        private readonly ILogger<KPlusFlightProvider> _logger;

        private const string CreateTokenPath = "kplus/v0/General.svc/Rest/Json/CreateTokenV2";
        private const string SearchAvailabilityPath = "kplus/v0/Air.svc/Rest/Json/SearchAvailability";

        public KPlusFlightProvider(HttpClient client, IOptions<KPlusOptions> options, ILogger<KPlusFlightProvider> logger)
        {
            _client = client;
            _options = options.Value;
            _logger = logger;
        }

        private async Task<string?> CreateTokenAsync(CancellationToken ct)
        {
            try
            {
                var req = new KPlusCreateTokenRequest
                {
                    ChannelCredential = new KPlusChannelCredential
                    {
                        ChannelCode = _options.ChannelCode,
                        ChannelPassword = _options.ChannelPassword
                    }
                };

                var resp = await _client.PostAsJsonAsync(CreateTokenPath, req, ct);

                if (!resp.IsSuccessStatusCode)
                {
                    var txt = await resp.Content.ReadAsStringAsync(ct);
                    _logger.LogWarning("KPlus CreateToken failed {Status} {Body}", resp.StatusCode, txt);
                    return null;
                }

                var tokResp = await resp.Content.ReadFromJsonAsync<KPlusCreateTokenResponse>(cancellationToken: ct);
                if (tokResp == null || tokResp.HasError || tokResp.Result == null)
                {
                    _logger.LogWarning("KPlus CreateToken returned error: {err}", tokResp?.ErrorMessage);
                    return null;
                }

                return tokResp.Result.TokenCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating KPlus token");
                return null;
            }
        }

        public async Task<SingleResponseWrapper<FlightListingResponseDto>> SearchFlightsLitingAsync(GetFlightsListingQuery query, CancellationToken cancellationToken = default)
        {
            // 1) Acquire token
            var token = await CreateTokenAsync(cancellationToken);
            if (string.IsNullOrWhiteSpace(token))
            {
                return new SingleResponseWrapper<FlightListingResponseDto>
                {
                    Data = null,
                    Code = 500,
                    Message = "Failed to acquire KPlus token"
                };
            }

            try
            {
                // 2) Build search request mapping from GetFlightsListingQuery -> KPlusSearchRequest
                var searchReq = new KPlusSearchRequestWrapper
                {
                    Request = new KPlusSearchRequest
                    {
                        Token = null
                    }
                };

                // Map legs - support roundtrip (if Return_Date present)
                var legs = new List<KPlusLeg>();
                if (!string.IsNullOrWhiteSpace(query.Travel_Date))
                {
                    legs.Add(new KPlusLeg
                    {
                        DeparturePoint = new KPlusPoint { Code = query.Departure_Airport },
                        ArrivalPoint = new KPlusPoint { Code = query.Arrival_Airport },
                        Date = ParseToKPlusDate(query.Travel_Date)
                    });
                }
                if (!string.IsNullOrWhiteSpace(query.Return_Date))
                {
                    legs.Add(new KPlusLeg
                    {
                        DeparturePoint = new KPlusPoint { Code = query.Arrival_Airport },
                        ArrivalPoint = new KPlusPoint { Code = query.Departure_Airport },
                        Date = ParseToKPlusDate(query.Return_Date)
                    });
                }

                searchReq.Request.Legs = legs;

                // Map passengers
                var pax = new List<KPlusPassenger>();
                if (query.ADT > 0) pax.Add(new KPlusPassenger { Count = query.ADT.ToString(), PaxType = "0" });
                if (query.CNN > 0) pax.Add(new KPlusPassenger { Count = query.CNN.ToString(), PaxType = "1" });
                if (query.INF > 0) pax.Add(new KPlusPassenger { Count = query.INF.ToString(), PaxType = "2" });
                searchReq.Request.Passengers = pax;

                // Token
                searchReq.Request.Token = new KPlusTokenReference { TokenCode = token };

                // Advanced options example (optional)
                searchReq.Request.AdvancedOptions = new KPlusAdvancedOptions
                {
                    Air = new KPlusAirAdvanced
                    {
                        OnlyBestFares = false,
                        OnlyDirectFlights = false,
                        OnlyRefundableFlights = false
                    }
                };

                // 3) Call search API
                var resp = await _client.PostAsJsonAsync(SearchAvailabilityPath, searchReq, cancellationToken);

                if (!resp.IsSuccessStatusCode)
                {
                    var txt = await resp.Content.ReadAsStringAsync(cancellationToken: cancellationToken);
                    _logger.LogWarning("KPlus search failed {Status} {Body}", resp.StatusCode, txt);
                    return new SingleResponseWrapper<FlightListingResponseDto>
                    {
                        Data = null,
                        Code = (int)resp.StatusCode,
                        Message = "KPlus search failed"
                    };
                }

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var kplusResp = await resp.Content.ReadFromJsonAsync<KPlusSearchResponse>(options: options, cancellationToken: cancellationToken);

                if (kplusResp == null || kplusResp.HasError || kplusResp.Result == null)
                {
                    _logger.LogWarning("KPlus search returned no result or error: {err}", kplusResp?.ErrorMessage);
                    return new SingleResponseWrapper<FlightListingResponseDto>
                    {
                        Data = new FlightListingResponseDto { AllFlights = new List<FlightItemDto>() },
                        Code = 200,
                        Message = "No flights found"
                    };
                }

                // 4) Map minimal KPlusSearchResponse -> FlightListingResponseDto
                var flights = MapKPlusToFlightListing(kplusResp);

                var result = new FlightListingResponseDto
                {
                    AllFlights = flights
                };

                // Compute cheapest, fastest, best like your Mosafir provider (reuse same helpers if possible)
                // For brevity here we'll compute cheapest and fastest basic versions:

                var withMetrics = flights.Select(f => new
                {
                    Flight = f,
                    Price = SafePrice(f.Total_Price),
                    Duration = GetTotalDuration(f)
                }).ToList();

                result.Cheapest = withMetrics.OrderBy(x => x.Price).FirstOrDefault()?.Flight;
                result.Fastest = withMetrics.OrderBy(x => x.Duration).FirstOrDefault()?.Flight;

                // Best: normalize price + duration (same simple heuristic)
                if (withMetrics.Any())
                {
                    var minPrice = withMetrics.Min(m => m.Price);
                    var minMinutes = withMetrics.Min(m => m.Duration.TotalMinutes);
                    var best = withMetrics
                             .Select(x => new
                             {
                                 x.Flight,
                                 Score =
                                     (x.Price / (minPrice + 1)) +
                                     ((decimal)x.Duration.TotalMinutes / ((decimal)minMinutes + 1))
                             })
                             .OrderBy(x => x.Score)
                             .FirstOrDefault()?.Flight;

                    result.Best = best;
                }

                return new SingleResponseWrapper<FlightListingResponseDto>
                {
                    Data = result,
                    Code = 200,
                    Message = "Data fetched successfully from KPlus"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling KPlus search");
                return new SingleResponseWrapper<FlightListingResponseDto>
                {
                    Data = null,
                    Code = 500,
                    Message = "Error calling KPlus"
                };
            }
        }

        #region Helpers

        private static string ParseToKPlusDate(string input)
        {
            // KPlus sample shows format "02.09.2023" in example — we will attempt to parse the incoming date and format dd.MM.yyyy if possible,
            // otherwise return as-is.
            if (DateTime.TryParse(input, out var dt))
                return dt.ToString("dd.MM.yyyy");
            return input;
        }

        private static decimal SafePrice(string? price)
            => decimal.TryParse(price, out var p) ? p : decimal.MaxValue;

        private static TimeSpan GetTotalDuration(FlightItemDto flight)
        {
            try
            {
                if (flight?.Sectors == null)
                    return TimeSpan.MaxValue;

                TimeSpan total = TimeSpan.Zero;
                foreach (var group in flight.Sectors)
                {
                    if (group == null) continue;
                    foreach (var sector in group)
                    {
                        if (sector == null) continue;
                        if (TimeSpan.TryParse(sector.Duration, out var value))
                        {
                            if (total < TimeSpan.MaxValue - value) total += value;
                            else return TimeSpan.MaxValue;
                        }
                        else
                        {
                            return TimeSpan.MaxValue;
                        }
                    }
                }
                return total;
            }
            catch { return TimeSpan.MaxValue; }
        }

        private List<FlightItemDto> MapKPlusToFlightListing(KPlusSearchResponse resp)
        {
            var list = new List<FlightItemDto>();

            var containers = resp.Result?.SearchResults ?? new List<KPlusSearchResultContainer>();
            foreach (var container in containers)
            {
                if (container.Results == null) continue;

                foreach (var item in container.Results)
                {
                    // Each ResultItem can contain several Fares — we'll map each fare to a FlightItem (or pick first)
                    if (item.Fares == null || !item.Fares.Any()) continue;

                    foreach (var fare in item.Fares)
                    {
                        var flightItem = new FlightItemDto
                        {
                            Total_Price = fare.TotalPrice?.TotalAmount.ToString("F2"),
                            Base_Fare = fare.TotalPrice?.TotalAmount.ToString("F2"), // minimal mapping
                            Taxes = "0",
                            Passengers = new PassengerInfo
                            {
                                Adt = fare.PassengerFares?.FirstOrDefault(p => p.PassengerType == 0)?.Count ?? 0,
                                Cnn = fare.PassengerFares?.FirstOrDefault(p => p.PassengerType == 1)?.Count ?? 0,
                                Inf = fare.PassengerFares?.FirstOrDefault(p => p.PassengerType == 2)?.Count ?? 0
                            },
                            Sectors = new List<List<FlightSector>>()
                        };

                        // Map legs & segments (best effort)
                        if (item.Legs != null && item.Legs.Any())
                        {
                            foreach (var legGroup in item.Legs)
                            {
                                var sectorList = new List<FlightSector>();

                                // Prefer AlternativeLegs -> FareSegments for concrete segments
                                var altLegs = legGroup.AlternativeLegs;
                                if (altLegs != null)
                                {
                                    foreach (var alt in altLegs)
                                    {
                                        if (alt?.FareSegments == null) continue;
                                        foreach (var seg in alt.FareSegments)
                                        {
                                            var sector = new FlightSector
                                            {
                                                Flight_Number = seg?.FlightNo,
                                                //Departure_Air_Port = seg?.DepartureAirport?.Code,
                                                Origin_Air_Port = seg?.DepartureAirport?.Code,
                                                Destination_Air_Port = seg?.ArrivalAirport?.Code,
                                                Travel_Date = TryParseDate(seg?.DepartureDate),
                                                Arrival_Date = TryParseDate(seg?.ArrivalDate),
                                                Duration = seg?.FlightDuration > 0 ? TimeSpan.FromMinutes(seg.FlightDuration).ToString() : string.Empty,
                                                Origin = seg?.DepartureAirport?.Code,
                                                Destination = seg?.ArrivalAirport?.Code
                                            };
                                            sectorList.Add(sector);
                                        }
                                    }
                                }
                                else if (legGroup.SearchLeg != null)
                                {
                                    // best-effort mapping — SearchLeg contains date + points
                                    var s = legGroup.SearchLeg;
                                    var sector = new FlightSector
                                    {
                                        Travel_Date = TryParseDate(s.Date),
                                        Origin = s.DeparturePoint?.Code,
                                        Destination = s.ArrivalPoint?.Code
                                    };
                                    sectorList.Add(sector);
                                }

                                if (sectorList.Any()) flightItem.Sectors.Add(sectorList);
                            }
                        }

                        list.Add(flightItem);
                    }
                }
            }

            return list;
        }

        private static DateTime? TryParseDate(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;
            if (DateTime.TryParse(value, out var dt)) return dt;
            // Try dd.MM.yyyy
            if (DateTime.TryParseExact(value, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out dt))
                return dt;
            return null;
        }

        #endregion
    }
}
