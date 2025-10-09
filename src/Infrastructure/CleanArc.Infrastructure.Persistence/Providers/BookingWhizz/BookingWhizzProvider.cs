using CleanArc.Application.Contracts.Mappers;
using CleanArc.Application.Contracts.Providers;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Entities.Hotel;
using CleanArc.Domain.Entities.SearchHotelDetail;
using CleanArc.Domain.Enums;
using CleanArc.Infrastructure.Persistence.Configuration.HotelProvidersConfig;
using CleanArc.Infrastructure.Persistence.Helpers;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CleanArc.Infrastructure.Persistence.Providers.BookingWhizz
{
    //Implements IHotelProvider, calls 3rd party API
    public class BookingWhizzProvider : IHotelProvider
    {
        private readonly HttpClient _httpClient;
        private readonly IHotelResponseMapper<XDocument> _mapper;
        private readonly BookingWhizzSettings _settings;

        public APIProvider ProviderName => APIProvider.BookingWhizz;

        public BookingWhizzProvider(HttpClient httpClient, IHotelResponseMapper<XDocument> mapper,
            IOptions<BookingWhizzSettings> settings)
        {
            _httpClient = httpClient;
            _mapper = mapper;
            _settings = settings.Value;
        }

        public async Task<List<SearchDetail>> SearchHotelsAsync(CustomizedSearchRequest request)
        {
            string cityName = request.FilterArray?
                .FirstOrDefault(f => f.ParameterName.Equals("name", StringComparison.OrdinalIgnoreCase))?
                .ParameterValue ?? "Islamabad";

            var url = $"{_settings.BaseUrl}getaccommodationsearchtest?" +
                      $"userid={_settings.UserId}&password={_settings.Password}" +
                      $"&cityname={cityName}&checkin={request.StartDate:yyyy-MM-dd}" +
                      $"&checkout={request.EndDate:yyyy-MM-dd}&multilanguageid={_settings.MultiLanguageId}" +
                      $"&agentid={_settings.AgentId}";

            var xmlString = await _httpClient.GetStringAsync(url);
            var xDoc = XDocument.Parse(xmlString);

            return _mapper.Map(xDoc,request.NoOfRooms,request.NoOfDays);
        }

        public async Task<HotelDetail> GetHotelDetailAsync(HotelDetailSearchRequest request)
        {
            int? accommodationId = request.GenericTitleId;
            string cityName=request.CityName;
            var checkIn = request.StartDate?.ToString("yyyy-MM-dd") ?? DateTime.Now.ToString("yyyy-MM-dd");
            var checkOut = request.EndDate?.ToString("yyyy-MM-dd") ?? DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");

            // Step 1: getaccommodationsearchtest
            var searchUrl = $"{_settings.BaseUrl}getaccommodationsearchtest?" +
                            $"userid={_settings.UserId}&password={_settings.Password}" +
                            $"&cityname={cityName}&checkin={checkIn}" +
                            $"&checkout={checkOut}&multilanguageid={_settings.MultiLanguageId}" +
                            $"&agentid={_settings.AgentId}";

            var searchXmlStr = await _httpClient.GetStringAsync(searchUrl);
            var searchXml = XDocument.Parse(searchXmlStr);
            //var hotelElement = searchXml.Descendants("result")
            //    .FirstOrDefault();
            var hotelElement = searchXml
                .Descendants("result")
                .FirstOrDefault(x =>
                    int.TryParse(x.Element("AccommodationId")?.Value, out var id) &&
                    //int.TryParse(x.Element("MinRoomId")?.Value, out var id) &&
                    id == request.GenericTitleId);
            if (hotelElement != null)
            {
                //accommodationId = int.Parse(hotelElement.Element("AccommodationId")?.Value ?? "0");
                // Step 2: getaccommodationdetail (for check-in/out time)
                //var detailUrl = $"{_settings.BaseUrl}getaccommodationdetail?" +
                //                $"userid={_settings.UserId}&password={_settings.Password}" +
                //                $"&accommodationid={accommodationId}&multilanguageid={_settings.MultiLanguageId}";

                //var detailXmlStr = await _httpClient.GetStringAsync(detailUrl);
                //var detailXml = XDocument.Parse(detailXmlStr);

                // Step 3: getavailability (room-level details)
                var availabilityUrl = $"{_settings.BaseUrl}getavailability?" +
                                      $"userid={_settings.UserId}&password={_settings.Password}" +
                                      $"&accommodationid={accommodationId}&checkin={checkIn}" +
                                      $"&checkout={checkOut}&multilanguageid={_settings.MultiLanguageId}";

                var availabilityXmlStr = await _httpClient.GetStringAsync(availabilityUrl);
                var availabilityXml = XDocument.Parse(availabilityXmlStr);

                // Step 4: Map all 3 sources
                var hotelDetail = BookingWhizzRoomDetailMapper.MapHotelDetail(hotelElement, /*detailXml,*/ availabilityXml, request.NoOfRooms, request.NoOfDays);

                return hotelDetail;
            }
            else { return null; }
        }
    }



}
