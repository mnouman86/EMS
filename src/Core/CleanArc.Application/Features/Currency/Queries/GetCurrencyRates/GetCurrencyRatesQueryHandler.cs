using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.Extensions;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging; using CleanArc.Domain.Common;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArc.Application.Features.Activity.Queries.GetAllActivity;
using CleanArc.Domain.Interfaces.Services;
using CleanArc.Domain.Entities.CurrencyRate;
using System.Globalization;

namespace CleanArc.Application.Features.Currency.Queries.GetAllCurrency
{
    internal class GetCurrencyRatesQueryHandler : IRequestHandler<GetCurrencyRatesQuery, OperationResult<List<GetCurrencyRatesQueryResult>>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCurrencyRatesQueryHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor
        private readonly IExchangeRateApiService _exchangeRateService;


        public GetCurrencyRatesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper,
            IHttpContextAccessor httpContextAccessor, ILogger<GetCurrencyRatesQueryHandler> logger,
            IExchangeRateApiService exchangeRateService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _exchangeRateService = exchangeRateService;
        }

        public async ValueTask<OperationResult<List<GetCurrencyRatesQueryResult>>> Handle(GetCurrencyRatesQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                var rates = await _exchangeRateService.GetCurrencyRates(request.baseCurrency);

                // Map to our response format with additional symbols
                var result = rates.Select(rate => new GetCurrencyRatesQueryResult
                {
                    CurrencyCode = rate.Key,
                    Rate = rate.Value,
                    DisplaySymbol = GetDisplaySymbol(rate.Key),
                    CountryFlagSymbol = GetCountryFlagSymbol(rate.Key)
                }).ToList();

                return OperationResult<List<GetCurrencyRatesQueryResult>>.SuccessResult(result);

            }
        }

        private string GetDisplaySymbol(string currencyCode)
        {
            // Add more symbols as needed
            return currencyCode switch
            {
                // Major currencies
                "USD" => "$",  // US Dollar
                "EUR" => "€",  // Euro
                "GBP" => "£",  // British Pound
                "JPY" => "¥",  // Japanese Yen
                "AUD" => "A$", // Australian Dollar
                "CAD" => "C$", // Canadian Dollar
                "CHF" => "CHF", // Swiss Franc
                "CNY" => "¥",  // Chinese Yuan (same as Yen)
                "HKD" => "HK$", // Hong Kong Dollar
                "NZD" => "NZ$", // New Zealand Dollar

                // Asian currencies
                "INR" => "₹",  // Indian Rupee
                "PKR" => "₨",  // Pakistani Rupee
                "BDT" => "৳",  // Bangladeshi Taka
                "LKR" => "Rs", // Sri Lankan Rupee
                "THB" => "฿",  // Thai Baht
                "SGD" => "S$", // Singapore Dollar
                "MYR" => "RM", // Malaysian Ringgit
                "IDR" => "Rp", // Indonesian Rupiah
                "PHP" => "₱",  // Philippine Peso
                "KRW" => "₩",  // South Korean Won
                "VND" => "₫",  // Vietnamese Dong
                "MMK" => "K",  // Myanmar Kyat
                "KHR" => "៛",  // Cambodian Riel
                "LAK" => "₭",  // Lao Kip
                "MNT" => "₮",  // Mongolian Tugrik
                "NPR" => "Rs", // Nepalese Rupee
                "BTN" => "Nu.", // Bhutanese Ngultrum
                "MVR" => "Rf", // Maldivian Rufiyaa

                // Middle Eastern currencies
                "AED" => "د.إ", // UAE Dirham
                "SAR" => "﷼",  // Saudi Riyal
                "QAR" => "ر.ق", // Qatari Riyal
                "OMR" => "ر.ع.", // Omani Rial
                "KWD" => "د.ك", // Kuwaiti Dinar
                "ILS" => "₪",  // Israeli Shekel
                "IRR" => "﷼",  // Iranian Rial
                "IQD" => "ع.د", // Iraqi Dinar
                "YER" => "﷼",  // Yemeni Rial
                "SYP" => "£",  // Syrian Pound
                "JOD" => "د.ا", // Jordanian Dinar
                "LBP" => "ل.ل", // Lebanese Pound
                "BHD" => ".د.ب", // Bahraini Dinar
                "TRY" => "₺",  // Turkish Lira

                // African currencies
                "ZAR" => "R",  // South African Rand
                "EGP" => "£",  // Egyptian Pound
                "NGN" => "₦",  // Nigerian Naira
                "KES" => "KSh", // Kenyan Shilling
                "ETB" => "Br", // Ethiopian Birr
                "GHS" => "₵",  // Ghanaian Cedi
                "MAD" => "د.م.", // Moroccan Dirham
                "TND" => "د.ت", // Tunisian Dinar
                "DZD" => "د.ج", // Algerian Dinar
                "SDG" => "£",  // Sudanese Pound
                "SOS" => "S",  // Somali Shilling
                "TZS" => "TSh", // Tanzanian Shilling
                "UGX" => "USh", // Ugandan Shilling
                "RWF" => "FRw", // Rwandan Franc
                "BIF" => "FBu", // Burundian Franc
                "XOF" => "CFA", // CFA Franc BCEAO
                "XAF" => "FCFA", // CFA Franc BEAC
                "CDF" => "FC", // Congolese Franc
                "GMD" => "D",  // Gambian Dalasi
                "GNF" => "FG", // Guinean Franc
                "LRD" => "L$", // Liberian Dollar
                "LSL" => "L",  // Lesotho Loti
                "MGA" => "Ar", // Malagasy Ariary
                "MUR" => "₨",  // Mauritian Rupee
                "MWK" => "MK", // Malawian Kwacha
                "NAD" => "N$", // Namibian Dollar
                "SCR" => "₨",  // Seychellois Rupee
                "SZL" => "L",  // Swazi Lilangeni
                "ZMW" => "ZK", // Zambian Kwacha
                "ZWL" => "Z$", // Zimbabwean Dollar

                // European currencies (non-Euro)
                "DKK" => "kr", // Danish Krone
                "NOK" => "kr", // Norwegian Krone
                "SEK" => "kr", // Swedish Krona
                "PLN" => "zł", // Polish Zloty
                "HUF" => "Ft", // Hungarian Forint
                "CZK" => "Kč", // Czech Koruna
                "RON" => "lei", // Romanian Leu
                "BGN" => "лв", // Bulgarian Lev
                "HRK" => "kn", // Croatian Kuna
                "RSD" => "дин", // Serbian Dinar
                "ALL" => "L",  // Albanian Lek
                "MKD" => "ден", // Macedonian Denar
                "BYN" => "Br", // Belarusian Ruble
                "MDL" => "L",  // Moldovan Leu
                "UAH" => "₴",  // Ukrainian Hryvnia
                "RUB" => "₽",  // Russian Ruble
                "GIP" => "£",  // Gibraltar Pound
                "FOK" => "kr", // Faroese Króna
                "GGP" => "£",  // Guernsey Pound
                "IMP" => "£",  // Isle of Man Pound
                "JEP" => "£",  // Jersey Pound

                // American currencies
                "MXN" => "$",  // Mexican Peso
                "BRL" => "R$", // Brazilian Real
                "ARS" => "$",  // Argentine Peso
                "CLP" => "$",  // Chilean Peso
                "COP" => "$",  // Colombian Peso
                "PEN" => "S/", // Peruvian Sol
                "BOB" => "Bs", // Bolivian Boliviano
                "PYG" => "₲",  // Paraguayan Guarani
                "UYU" => "$U", // Uruguayan Peso
                "VES" => "Bs", // Venezuelan Bolívar
                "DOP" => "RD$", // Dominican Peso
                "GTQ" => "Q",  // Guatemalan Quetzal
                "HNL" => "L",  // Honduran Lempira
                "NIO" => "C$", // Nicaraguan Córdoba
                "PAB" => "B/.", // Panamanian Balboa
                "BSD" => "B$", // Bahamian Dollar
                "BBD" => "Bds$", // Barbadian Dollar
                "BZD" => "BZ$", // Belize Dollar
                "TTD" => "TT$", // Trinidad and Tobago Dollar
                "XCD" => "EC$", // East Caribbean Dollar
                "ANG" => "ƒ",  // Netherlands Antillean Guilder
                "AWG" => "ƒ",  // Aruban Florin
                "KYD" => "CI$", // Cayman Islands Dollar
                "CRC" => "₡",  // Costa Rican Colón
                "CUP" => "$",  // Cuban Peso
                "HTG" => "G",  // Haitian Gourde
                "JMD" => "J$", // Jamaican Dollar

                // Other currencies
                "XPF" => "₣",  // CFP Franc
                "TOP" => "T$", // Tongan Paʻanga
                "WST" => "WS$", // Samoan Tala
                "FJD" => "FJ$", // Fijian Dollar
                "SBD" => "SI$", // Solomon Islands Dollar
                "VUV" => "VT", // Vanuatu Vatu
                "PGK" => "K",  // Papua New Guinean Kina
                "KID" => "$",  // Kiribati Dollar
                "TVD" => "$",  // Tuvaluan Dollar
                "MOP" => "MOP$", // Macanese Pataca
                "KZT" => "₸",  // Kazakhstani Tenge
                "UZS" => "so'm", // Uzbekistani Som
                "TJS" => "ЅМ", // Tajikistani Somoni
                "TMT" => "m",  // Turkmenistani Manat
                "AMD" => "֏",  // Armenian Dram
                "AZN" => "₼",  // Azerbaijani Manat
                "GEL" => "₾",  // Georgian Lari
                "BAM" => "KM", // Bosnia-Herzegovina Convertible Mark
                "ERN" => "Nfk", // Eritrean Nakfa
                "FKP" => "£",  // Falkland Islands Pound
                "SHP" => "£",  // Saint Helena Pound
                "SSP" => "£",  // South Sudanese Pound
                "STN" => "Db", // São Tomé and Príncipe Dobra
                "CVE" => "$",  // Cape Verdean Escudo
                "KMF" => "CF", // Comorian Franc
                "DJF" => "Fdj", // Djiboutian Franc
                "MRU" => "UM", // Mauritanian Ouguiya
                "MZN" => "MT", // Mozambican Metical
                "SRD" => "$",  // Surinamese Dollar
                "BND" => "B$", // Brunei Dollar
                "BWP" => "P",  // Botswana Pula
                "SLE" => "Le", // Sierra Leonean Leone
                "SLL" => "Le", // Sierra Leonean Leone (old)
                "XDR" => "SDR", // IMF Special Drawing Rights
                "AOA" => "Kz", // Angolan Kwanza               

                // Default to currency code if no symbol is defined
                _ => currencyCode
            };
        }

        private string GetCountryFlagSymbol(string currencyCode)
        {
            return currencyCode switch
            {
                // Major currencies
                "USD" => "🇺🇸", // US Dollar
                "EUR" => "🇪🇺", // Euro (European Union)
                "GBP" => "🇬🇧", // British Pound
                "JPY" => "🇯🇵", // Japanese Yen
                "AUD" => "🇦🇺", // Australian Dollar
                "CAD" => "🇨🇦", // Canadian Dollar
                "CHF" => "🇨🇭", // Swiss Franc
                "CNY" => "🇨🇳", // Chinese Yuan
                "HKD" => "🇭🇰", // Hong Kong Dollar
                "NZD" => "🇳🇿", // New Zealand Dollar

                // Asian currencies
                "INR" => "🇮🇳", // Indian Rupee
                "PKR" => "🇵🇰", // Pakistani Rupee
                "BDT" => "🇧🇩", // Bangladeshi Taka
                "LKR" => "🇱🇰", // Sri Lankan Rupee
                "NPR" => "🇳🇵", // Nepalese Rupee
                "THB" => "🇹🇭", // Thai Baht
                "SGD" => "🇸🇬", // Singapore Dollar
                "MYR" => "🇲🇾", // Malaysian Ringgit
                "IDR" => "🇮🇩", // Indonesian Rupiah
                "PHP" => "🇵🇭", // Philippine Peso
                "VND" => "🇻🇳", // Vietnamese Dong
                "KRW" => "🇰🇷", // South Korean Won
                "MMK" => "🇲🇲", // Myanmar Kyat
                "KHR" => "🇰🇭", // Cambodian Riel
                "LAK" => "🇱🇦", // Lao Kip
                "MNT" => "🇲🇳", // Mongolian Tugrik
                "BTN" => "🇧🇹", // Bhutanese Ngultrum
                "MVR" => "🇲🇻", // Maldivian Rufiyaa

                // Middle Eastern currencies
                "AED" => "🇦🇪", // UAE Dirham
                "SAR" => "🇸🇦", // Saudi Riyal
                "QAR" => "🇶🇦", // Qatari Riyal
                "OMR" => "🇴🇲", // Omani Rial
                "KWD" => "🇰🇼", // Kuwaiti Dinar
                "ILS" => "🇮🇱", // Israeli Shekel
                "TRY" => "🇹🇷", // Turkish Lira
                "IRR" => "🇮🇷", // Iranian Rial
                "IQD" => "🇮🇶", // Iraqi Dinar
                "YER" => "🇾🇪", // Yemeni Rial
                "SYP" => "🇸🇾", // Syrian Pound
                "JOD" => "🇯🇴", // Jordanian Dinar
                "LBP" => "🇱🇧", // Lebanese Pound
                "BHD" => "🇧🇭", // Bahraini Dinar

                // African currencies
                "ZAR" => "🇿🇦", // South African Rand
                "EGP" => "🇪🇬", // Egyptian Pound
                "NGN" => "🇳🇬", // Nigerian Naira
                "KES" => "🇰🇪", // Kenyan Shilling
                "ETB" => "🇪🇹", // Ethiopian Birr
                "GHS" => "🇬🇭", // Ghanaian Cedi
                "MAD" => "🇲🇦", // Moroccan Dirham
                "TND" => "🇹🇳", // Tunisian Dinar
                "DZD" => "🇩🇿", // Algerian Dinar
                "SDG" => "🇸🇩", // Sudanese Pound
                "SOS" => "🇸🇴", // Somali Shilling
                "TZS" => "🇹🇿", // Tanzanian Shilling
                "UGX" => "🇺🇬", // Ugandan Shilling
                "RWF" => "🇷🇼", // Rwandan Franc
                "BIF" => "🇧🇮", // Burundian Franc
                "XOF" => "🇧🇯🇧🇫🇨🇮🇬🇳🇲🇱🇳🇪🇸🇳🇹🇬", // CFA Franc BCEAO
                "XAF" => "🇨🇲🇨🇫🇹🇩🇬🇶🇬🇦🇨🇬", // CFA Franc BEAC
                "CDF" => "🇨🇩", // Congolese Franc
                "GMD" => "🇬🇲", // Gambian Dalasi
                "GNF" => "🇬🇳", // Guinean Franc
                "LRD" => "🇱🇷", // Liberian Dollar
                "LSL" => "🇱🇸", // Lesotho Loti
                "MGA" => "🇲🇬", // Malagasy Ariary
                "MRO" => "🇲🇷", // Mauritanian Ouguiya
                "MUR" => "🇲🇺", // Mauritian Rupee
                "MWK" => "🇲🇼", // Malawian Kwacha
                "NAD" => "🇳🇦", // Namibian Dollar
                "SCR" => "🇸🇨", // Seychellois Rupee
                "SZL" => "🇸🇿", // Swazi Lilangeni
                "ZMW" => "🇿🇲", // Zambian Kwacha
                "ZWL" => "🇿🇼", // Zimbabwean Dollar

                // European currencies (non-Euro)
                "DKK" => "🇩🇰", // Danish Krone
                "NOK" => "🇳🇴", // Norwegian Krone
                "SEK" => "🇸🇪", // Swedish Krona
                "PLN" => "🇵🇱", // Polish Zloty
                "HUF" => "🇭🇺", // Hungarian Forint
                "CZK" => "🇨🇿", // Czech Koruna
                "RON" => "🇷🇴", // Romanian Leu
                "BGN" => "🇧🇬", // Bulgarian Lev
                "HRK" => "🇭🇷", // Croatian Kuna
                "RSD" => "🇷🇸", // Serbian Dinar
                "ALL" => "🇦🇱", // Albanian Lek
                "MKD" => "🇲🇰", // Macedonian Denar
                "BYN" => "🇧🇾", // Belarusian Ruble
                "MDL" => "🇲🇩", // Moldovan Leu
                "UAH" => "🇺🇦", // Ukrainian Hryvnia
                "RUB" => "🇷🇺", // Russian Ruble
                "GIP" => "🇬🇮", // Gibraltar Pound
                "FOK" => "🇫🇴", // Faroese Króna
                "GGP" => "🇬🇬", // Guernsey Pound
                "IMP" => "🇮🇲", // Isle of Man Pound
                "JEP" => "🇯🇪", // Jersey Pound

                // American currencies
                "MXN" => "🇲🇽", // Mexican Peso
                "BRL" => "🇧🇷", // Brazilian Real
                "ARS" => "🇦🇷", // Argentine Peso
                "CLP" => "🇨🇱", // Chilean Peso
                "COP" => "🇨🇴", // Colombian Peso
                "PEN" => "🇵🇪", // Peruvian Sol
                "BOB" => "🇧🇴", // Bolivian Boliviano
                "PYG" => "🇵🇾", // Paraguayan Guarani
                "UYU" => "🇺🇾", // Uruguayan Peso
                "VES" => "🇻🇪", // Venezuelan Bolívar
                "DOP" => "🇩🇴", // Dominican Peso
                "GTQ" => "🇬🇹", // Guatemalan Quetzal
                "HNL" => "🇭🇳", // Honduran Lempira
                "NIO" => "🇳🇮", // Nicaraguan Córdoba
                "PAB" => "🇵🇦", // Panamanian Balboa
                "BSD" => "🇧🇸", // Bahamian Dollar
                "BBD" => "🇧🇧", // Barbadian Dollar
                "BZD" => "🇧🇿", // Belize Dollar
                "TTD" => "🇹🇹", // Trinidad and Tobago Dollar
                "XCD" => "🇦🇬🇩🇲🇬🇩🇲🇸🇰🇳🇱🇨🇻🇨", // East Caribbean Dollar
                "ANG" => "🇨🇼🇸🇽", // Netherlands Antillean Guilder
                "AWG" => "🇦🇼", // Aruban Florin
                "KYD" => "🇰🇾", // Cayman Islands Dollar
                "CRC" => "🇨🇷", // Costa Rican Colón
                "CUP" => "🇨🇺", // Cuban Peso
                "HTG" => "🇭🇹", // Haitian Gourde
                "JMD" => "🇯🇲", // Jamaican Dollar

                // Other currencies
                "XPF" => "🇵🇫🇳🇨🇼🇫", // CFP Franc (French territories)
                "TOP" => "🇹🇴", // Tongan Paʻanga
                "WST" => "🇼🇸", // Samoan Tala
                "FJD" => "🇫🇯", // Fijian Dollar
                "SBD" => "🇸🇧", // Solomon Islands Dollar
                "VUV" => "🇻🇺", // Vanuatu Vatu
                "PGK" => "🇵🇬", // Papua New Guinean Kina
                "KID" => "🇰🇮", // Kiribati Dollar
                "TVD" => "🇹🇻", // Tuvaluan Dollar
                "MOP" => "🇲🇴", // Macanese Pataca
                "KZT" => "🇰🇿", // Kazakhstani Tenge
                "UZS" => "🇺🇿", // Uzbekistani Som
                "TJS" => "🇹🇯", // Tajikistani Somoni
                "TMT" => "🇹🇲", // Turkmenistani Manat
                "AMD" => "🇦🇲", // Armenian Dram
                "AZN" => "🇦🇿", // Azerbaijani Manat
                "GEL" => "🇬🇪", // Georgian Lari
                "BAM" => "🇧🇦", // Bosnia-Herzegovina Convertible Mark
                "ERN" => "🇪🇷", // Eritrean Nakfa
                "FKP" => "🇫🇰", // Falkland Islands Pound
                "SHP" => "🇸🇭", // Saint Helena Pound
                "SSP" => "🇸🇸", // South Sudanese Pound
                "STN" => "🇸🇹", // São Tomé and Príncipe Dobra
                "CVE" => "🇨🇻", // Cape Verdean Escudo
                "KMF" => "🇰🇲", // Comorian Franc
                "DJF" => "🇩🇯", // Djiboutian Franc
                "MRU" => "🇲🇷", // Mauritanian Ouguiya
                "MZN" => "🇲🇿", // Mozambican Metical
                "SRD" => "🇸🇷", // Surinamese Dollar
                "BND" => "🇧🇳", // Brunei Dollar
                "BWP" => "🇧🇼", // Botswana Pula
                "SLE" => "🇸🇱", // Sierra Leonean Leone
                "SLL" => "🇸🇱", // Sierra Leonean Leone (old)
                "XDR" => "🌍", // IMF Special Drawing Rights

                // Fallback for unknown currencies
                _ => GetFlagByCurrencyCode(currencyCode) ?? "🏳"
            };
        }

        private string GetFlagByCurrencyCode(string currencyCode)
        {
            // Many currency codes use ISO country codes as their first two letters
            if (currencyCode.Length >= 2)
            {
                var countryCode = currencyCode.Substring(0, 2);
                try
                {
                    var region = new RegionInfo(countryCode);
                    return GetFlagEmoji(region.TwoLetterISORegionName);
                }
                catch
                {
                    // If we can't find by country code, try our secondary mapping
                    return SecondaryCurrencyMapping.GetValueOrDefault(currencyCode, null);
                }
            }
            return null;
        }

        private string GetFlagEmoji(string countryCode)
        {
            // Convert ISO country code to flag emoji
            return string.Concat(countryCode.ToUpper().Select(c => char.ConvertFromUtf32(c + 0x1F1A5)));
        }

        // Secondary mapping for currencies that don't match country codes
        private static readonly Dictionary<string, string> SecondaryCurrencyMapping = new()
{
    {"RUB", "🇷🇺"}, // Russian Ruble
    {"UAH", "🇺🇦"}, // Ukrainian Hryvnia
    {"BYN", "🇧🇾"}, // Belarusian Ruble
    {"KZT", "🇰🇿"}, // Kazakhstani Tenge
    {"UZS", "🇺🇿"}, // Uzbekistani Som
    {"GEL", "🇬🇪"}, // Georgian Lari
    {"AMD", "🇦🇲"}, // Armenian Dram
    {"AZN", "🇦🇿"}, // Azerbaijani Manat
    // Add more as needed
};
    }

}
