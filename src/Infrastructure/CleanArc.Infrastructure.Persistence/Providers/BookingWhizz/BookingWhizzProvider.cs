using CleanArc.Application.Contracts.Mappers;
using CleanArc.Application.Contracts.Providers;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Entities.SearchHotelDetail;
using CleanArc.Infrastructure.Persistence.Configuration.HotelProvidersConfig;
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


        public string ProviderName => "BookingWhizz";

        public BookingWhizzProvider(HttpClient httpClient, IHotelResponseMapper<XDocument> mapper,
            IOptions<BookingWhizzSettings> settings)
        {
            _httpClient = httpClient;
            _mapper = mapper;
            _settings = settings.Value;
        }
        public async Task<List<SearchDetail>> SearchHotelsAsync(CustomizedSearchRequest request)
        {
            //var query = $"Connect.svc/xml/getaccommodationsearchtest" +
            //            $"?userid=00000" +
            //            $"&password=AAAAAAAA" +
            //            $"&cityname={Uri.EscapeDataString(request.Name)}" +
            //            $"&checkin={request.StartDate:yyyy-MM-dd}" +
            //            $"&checkout={request.EndDate:yyyy-MM-dd}" +
            //            $"&multilanguageid=1" +
            //            $"&agentid=11";
            var url = $"{_settings.BaseUrl}getaccommodationsearchtest?" +
                 $"userid={_settings.UserId}&password={_settings.Password}" +
                 $"&cityname={request.Name}&checkin={request.StartDate:yyyy-MM-dd}" +
                 $"&checkout={request.EndDate:yyyy-MM-dd}&multilanguageid={_settings.MultiLanguageId}" +
                 $"&agentid={_settings.AgentId}";

            var xmlString = await _httpClient.GetStringAsync(url);
            var xDoc = XDocument.Parse(xmlString);

            return _mapper.Map(xDoc);
        }
        //public async Task<List<SearchDetail>> SearchHotelsAsync(CustomizedSearchRequest request)
        //{
        //    //string url = $"http://beapi.bookingwhizz.com/Connect.svc/xml/getaccommodationsearchtest?userid=00000&password=AAAAAAAA&cityname={request.Name}&checkin={request.StartDate:yyyy-MM-dd}&checkout={request.EndDate:yyyy-MM-dd}&multilanguageid=1&agentid=11";


        //    var xmlString = await _httpClient.GetStringAsync(url);
        //    var xDoc = XDocument.Parse(xmlString);

        //    return _mapper.Map(xDoc);
        //}
    }
}
