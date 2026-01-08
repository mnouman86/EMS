using CleanArc.Application.Contracts.Mappers;
using CleanArc.Application.Contracts.Providers;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Entities.Hotel;
using CleanArc.Domain.Entities.RoomDetails;
using CleanArc.Domain.Entities.SearchHotelDetail;
using CleanArc.Domain.Enums;
using CleanArc.Infrastructure.Persistence.Configuration.HotelProvidersConfig;
using CleanArc.Infrastructure.Persistence.Helpers;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
//http://beapi.bookingwhizz.com/Connect.svc/xml/getaccommodationsearchtest?userid=10037&password=KUYHrtghd@%$&cityname=Lahore&checkin=2025-10-23&checkout=2025-10-24&multilanguageid=1&converted_currency=PKR&agentid=37
//http://beapi.bookingwhizz.com/Connect.svc/xml/getaccommodationsearchtest?userid=10037&password=KUYHrtghd@%$&cityname=Lahore&checkin=2025-10-23&checkout=2025-10-24&offset=0&limit=10&multilanguageid=1&converted_currency=PKR&agentid=37
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

        //  "BaseUrl": "http://beapi.bookingwhizz.com/Connect.svc/xml/",
        //"UserId": "10037",
        //"Password": "KUYHrtghd@%$",
        //"AgentId": "37",
        //"MultiLanguageId": "1"

        
        public async Task<List<SearchDetail>> SearchHotelsAsync(CustomizedSearchRequest request)
        {
            string cityName = request.FilterArray?
                .FirstOrDefault(f => f.ParameterName.Equals("name", StringComparison.OrdinalIgnoreCase))?
                .ParameterValue ?? string.Empty;

            int limit = request.PageSize > 0 ? request.PageSize : 1000;
            int offSet = request.PageNumber-1 > 0 ? request.PageNumber * limit : 0;
            string paginationFilter = "";
            if (request.PageSize > 0 && request.PageNumber - 1>0)
            {
                paginationFilter = $"&offset={offSet}&limit={limit}";
            }
            
            string priceRangeFilter = "";
            if (request.MinPrice>=0 && request.MaxPrice>0 && request.MaxPrice>request.MinPrice)
            {
                priceRangeFilter = $"&pricerangestart={request.MinPrice}&pricerangeend={request.MaxPrice}";
            }

            string columnName = "4";
            string columnDirection = null;

            if (request?.SortingArray != null && request.SortingArray.Any())
            {
                // Extract the first sorting parameter
                var sorting = request.SortingArray.First();

                //columnName = sorting.SortingColumnName;
                columnDirection = sorting.SortingColumnDirection?.ToUpper() == "DESC" ? "1" : "0";
            }
            else
            {
                // Default sorting (optional)
                columnName = "4";
                columnDirection = "0";
            }

            string adultFilter = "";
            if (request.NoOfAdults > 0)
            {
                adultFilter = $"&Adults={request.NoOfAdults}";
            }
            string roomsFilter = "";
            if (request.NoOfRooms > 0)
            {
            
                roomsFilter = $"&rooms={request.NoOfRooms}";
            }

            string amenitiesFilter = "";
            if (request.Amenities?.Trim()!=string.Empty)
            {
                amenitiesFilter = $"&facilityname={request.Amenities?.Trim()}";
            }

            string cityNameFilter = "";
            if (cityName != string.Empty)
            {
                cityNameFilter = $"&cityname={cityName}";
            }
            string accomodationNameFilter = "";
            if (request.Name != string.Empty)
            {
                accomodationNameFilter = $"&accommodationname={request.Name}";
            }

            var url = $"{_settings.BaseUrl}getaccommodationsearchnew?" +
                      $"userid={_settings.UserId}&password={_settings.Password}" +
                      //$"&checkin={request.StartDate:yyyy-MM-dd}" +
                      $"{cityNameFilter}&checkin={request.StartDate:yyyy-MM-dd}" +
                      $"&checkout={request.EndDate:yyyy-MM-dd}&sortby={columnName}&sort={columnDirection}"+
                      //$"&offset={offSet}&limits={limit}{priceRangeFilter}{adultFilter}{roomsFilter}{amenitiesFilter}&multilanguageid={_settings.MultiLanguageId}" +
                      $"{paginationFilter}{priceRangeFilter}{adultFilter}{roomsFilter}{amenitiesFilter}{accomodationNameFilter}&multilanguageid={_settings.MultiLanguageId}" +
                      $"&converted_currency=PKR&agentid={_settings.AgentId}";
            //&accommodationtypename={request.PropertyType}
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
            var searchUrl = $"{_settings.BaseUrl}getaccommodationsearchnew?" +
                            $"userid={_settings.UserId}&password={_settings.Password}" +
                            $"&cityname={cityName}&checkin={checkIn}" +
                            $"&checkout={checkOut}&multilanguageid={_settings.MultiLanguageId}" +
                            $"&converted_currency=PKR&agentid={_settings.AgentId}";

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
                                      $"&checkout={checkOut}&currency=PKR&multilanguageid={_settings.MultiLanguageId}";

                var availabilityXmlStr = await _httpClient.GetStringAsync(availabilityUrl);
                var availabilityXml = XDocument.Parse(availabilityXmlStr);

                // Step 4: Map all 3 sources
                var hotelDetail = BookingWhizzRoomDetailMapper.MapHotelDetail(hotelElement, /*detailXml,*/ availabilityXml, request.NoOfRooms, request.NoOfDays);

                return hotelDetail;
            }
            else { return null; }
        }

        public async Task<BookingReservationResult> CreateReservationAsync(
    BookingReservationRequest request)
        {
            var queryParams = new Dictionary<string, string>
            {
                ["userid"] = _settings.UserId,
                ["password"] = _settings.Password,
                ["accommodationid"] = request.AccommodationId.ToString(),

                ["roomids"] = string.Join(",", request.RoomIds),
                ["rateplanids"] = string.Join(",", request.RatePlanIds),
                ["extraids"] = string.Join(",", request.RoomIds.Select(_ => "0")),
                ["roomqty"] = string.Join(",", request.RoomIds.Select(_ => "1")),

                ["checkin"] = request.CheckIn.ToString("yyyy-MM-dd"),
                ["checkout"] = request.CheckOut.ToString("yyyy-MM-dd"),

                ["booker_firstname"] = request.BookerFirstName,
                ["booker_lastname"] = request.BookerLastName ?? "",
                ["booker_email"] = request.BookerEmail ?? "",
                ["booker_telephone"] = request.BookerTelephone ?? "",

                ["booker_street"] = request.BookerStreet,
                ["booker_city"] = request.BookerCity,
                ["booker_country"] = request.BookerCountry,
                ["booker_zipcode"] = request.BookerZipCode,

                ["guest_qtys"] = string.Join(",", request.RoomIds.Select(_ => request.GuestQty.ToString())),
                ["guest_names"] = string.Join(",", request.GuestNames),

                ["totalprice"] = request.TotalPrice.ToString("0.00"),
                ["ratesbydate"] = string.Join(",", request.RatesByDate.Select(r => r.ToString("0.00"))),

                ["payment_method"] = request.PaymentMethod,
                ["charged_amount"] = request.TotalPrice.ToString("0.00"),

                ["sourceid"] = request.SourceId,
                ["multilanguageid"] = _settings.MultiLanguageId
            };

            var url = QueryHelpers.AddQueryString(
                $"{_settings.BaseUrl}createreservation",
                queryParams);

            var responseXml = await _httpClient.GetStringAsync(url);
            var xDoc = XDocument.Parse(responseXml);

            return ParseReservationResponse(xDoc);
        }

        private BookingReservationResult ParseReservationResponse(XDocument xDoc)
        {
            var resultNode = xDoc.Descendants("Result").FirstOrDefault();

            if (resultNode == null)
            {
                return new BookingReservationResult
                {
                    IsSuccess = false,
                    Message = "BookingWhizz reservation failed"
                };
            }

            return new BookingReservationResult
            {
                IsSuccess = true,
                BookingId = resultNode.Element("BookingID")?.Value,
                PinCode = resultNode.Element("PinCode")?.Value,
                Message = resultNode.Element("Message")?.Value
            };
        }

    }



}
