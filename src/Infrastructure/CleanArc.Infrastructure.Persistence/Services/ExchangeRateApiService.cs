using CleanArc.Application.Contracts.Persistence;
using CleanArc.Domain.Entities.OTP;
using CleanArc.Domain.Enums;
using CleanArc.Domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
namespace CleanArc.Infrastructure.Persistence.Services
{
    public class ExchangeRateApiService : IExchangeRateApiService
    {
        private readonly HttpClient _httpClient;
        //private const string ApiKey = "765fcb56b5f7bdb9e4dd8c8e"; // nouman.rafique
        private const string ApiKey = "fe8edf8ecbbf566828c86dbd"; // temporary

        public ExchangeRateApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://v6.exchangerate-api.com/v6/");
        }

        public async Task<Dictionary<string, decimal>> GetCurrencyRates(string baseCurrency)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{ApiKey}/latest/{baseCurrency}");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var result = JsonSerializer.Deserialize<ExchangeRateApiResponse>(content, options);

                if (result?.Result != "success" || result.ConversionRates == null)
                {
                    throw new ApplicationException("Failed to fetch currency rates: API returned unsuccessful result");
                }

                return result.ConversionRates;
            }
            catch (Exception ex)
            {
                // Log error
                throw new ApplicationException("Failed to fetch currency rates", ex);
            }
        }

        private class ExchangeRateApiResponse
        {
            [JsonPropertyName("result")]
            public string Result { get; set; }

            [JsonPropertyName("base_code")]
            public string BaseCode { get; set; }

            [JsonPropertyName("conversion_rates")]
            public Dictionary<string, decimal> ConversionRates { get; set; }

            [JsonPropertyName("time_last_update_utc")]
            public string LastUpdateTime { get; set; }

            [JsonPropertyName("time_next_update_utc")]
            public string NextUpdateTime { get; set; }

            // You can include other fields if needed
            [JsonPropertyName("documentation")]
            public string Documentation { get; set; }
        }
    }
}
