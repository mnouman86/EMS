using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Flights;
using CleanArc.Infrastructure.Persistence.HttpModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Persistence.Services
{
    public class JsonLookupService : ILookupService
    {
        private readonly ILogger<JsonLookupService> _logger;
        private readonly string _airportsPath;
        private readonly string _airlinesPath;
        private readonly string _logoBaseUrl; // relative url base for logos e.g. "/airlinelogos/"

        // in constructor we set paths; files can be in wwwroot/data or /mnt/data for dev
        public JsonLookupService(ILogger<JsonLookupService> logger, IConfiguration cfg)
        {
            _logger = logger;

            var dataFolder = cfg["Lookup:DataFolder"];
            if (string.IsNullOrWhiteSpace(dataFolder))
            {
                // fallback to relative path if not configured
                dataFolder = Path.Combine(AppContext.BaseDirectory, "data");
            }

            _airportsPath = Path.Combine(dataFolder, "airports_org.json");
            _airlinesPath = Path.Combine(dataFolder, "airlines.json");
            _logoBaseUrl = cfg["Lookup:LogoBaseUrl"] ?? "/airlinelogos/";
        }

        private async Task<string> ReadAllTextAsync(string path, CancellationToken ct)
        {
            if (!File.Exists(path))
            {
                _logger.LogWarning("Lookup file not found: {Path}", path);
                return string.Empty;
            }
            using var fs = File.OpenRead(path);
            using var sr = new StreamReader(fs);
            return await sr.ReadToEndAsync();
        }

        public async Task<IReadOnlyList<AirportDto>> GetAirportsAsync(CancellationToken ct = default)
        {
            var text = await ReadAllTextAsync(_airportsPath, ct);
            if (string.IsNullOrWhiteSpace(text)) return Array.Empty<AirportDto>();

            // airports_org.json is a JSON array of objects with "code" and "name"
            try
            {
                using var doc = JsonDocument.Parse(text);
                var arr = doc.RootElement;
                var list = new List<AirportDto>();
                foreach (var el in arr.EnumerateArray())
                {
                    var code = el.GetProperty("code").GetString() ?? string.Empty;
                    var name = el.GetProperty("name").GetString() ?? string.Empty;
                    if (!string.IsNullOrWhiteSpace(code))
                        list.Add(new AirportDto(code, name));
                }
                return list;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to parse airports json at {Path}", _airportsPath);
                return Array.Empty<AirportDto>();
            }
        }

        // airlines.json is a long list (in your file it's a JSON array of CSV-like strings or a single string).
        // We'll parse defensively.
        public async Task<AirlineInfoDto?> GetAirlineInfoAsync(string airlineCode, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(airlineCode)) return null;
            airlineCode = airlineCode.Trim().ToUpperInvariant();

            var text = await ReadAllTextAsync(_airlinesPath, ct);
            if (string.IsNullOrWhiteSpace(text)) return null;

            // Attempt 1: parse JSON array of strings
            try
            {
                // If file is JSON array (["PA,Airblue","GF,Gulf Air"...])
                var parsed = JsonSerializer.Deserialize<string[]>(text);
                if (parsed != null)
                {
                    foreach (var entry in parsed)
                    {
                        var parts = entry.Split(',', 2);
                        if (parts.Length == 2 && string.Equals(parts[0].Trim(), airlineCode, StringComparison.OrdinalIgnoreCase))
                        {
                            var name = parts[1].Trim();
                            var logo = $"{_logoBaseUrl}{airlineCode}.png";
                            return new AirlineInfoDto(airlineCode, name, logo);
                        }
                    }
                }
            }
            catch { /* ignore - fallback to text parsing */ }

            // Fallback: treat as plain text and search for pattern "CODE,Name"
            // We'll search for `${code},` and extract the nearest name token after comma.
            var idx = text.IndexOf(airlineCode + ",", StringComparison.OrdinalIgnoreCase);
            if (idx >= 0)
            {
                // take substring from idx to next closing quote or next '","'
                var tail = text.Substring(idx);
                // find end delim either '",' or '",' or just comma delim
                var commaIdx = tail.IndexOf(',');
                if (commaIdx >= 0)
                {
                    var after = tail.Substring(commaIdx + 1);
                    // name ends at next quote or next comma+quote sequence
                    // crude extraction: take up to next quote or next comma that is followed by uppercase code pattern
                    var possibleName = after.Trim();
                    // take first few chars up to 80 and split by '",' or '","'
                    var endCandidates = new[] { "\",", "\",", "\"", "," };
                    int end = possibleName.Length;
                    foreach (var cand in endCandidates)
                    {
                        var p = possibleName.IndexOf(cand, StringComparison.Ordinal);
                        if (p > 0 && p < end) end = p;
                    }
                    var name = possibleName.Substring(0, Math.Min(end, possibleName.Length)).Trim().Trim(new[] { '"', ' ', '\n', '\r' });
                    var logo = $"{_logoBaseUrl}{airlineCode}.png";
                    return new AirlineInfoDto(airlineCode, name, logo);
                }
            }

            return null;
        }
    }
}
