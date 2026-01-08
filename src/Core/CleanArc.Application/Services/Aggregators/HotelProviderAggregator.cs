using CleanArc.Application.Contracts.Providers;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Entities.Hotel;
using CleanArc.Domain.Entities.RoomDetails;
using CleanArc.Domain.Entities.SearchHotelDetail;
using CleanArc.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Services.Aggregators
{
    //Uses all IHotelProviders and aggregates result
    public class HotelProviderAggregator
    {
        private readonly IEnumerable<IHotelProvider> _hotelProviders;

        public HotelProviderAggregator(IEnumerable<IHotelProvider> hotelProviders)
        {
            _hotelProviders = hotelProviders;
        }

        public async Task<List<SearchDetail>> SearchHotelsAsync(CustomizedSearchRequest request)
        {
            var tasks = _hotelProviders.Select(p => p.SearchHotelsAsync(request));
            var results = await Task.WhenAll(tasks);
            return results.SelectMany(x => x).ToList();
        }

        public async Task<HotelDetail?> GetHotelDetailAsync(HotelDetailSearchRequest request, APIProvider providerName)
        {
            var provider = _hotelProviders.FirstOrDefault(p =>
                p.ProviderName.Equals(providerName));

            return provider != null
                ? await provider.GetHotelDetailAsync(request)
                : null;
        }

        
        public async Task<BookingReservationResult?> CreateReservationAsync(BookingReservationRequest request, APIProvider providerName)
        {
            var provider = _hotelProviders.FirstOrDefault(p =>
                p.ProviderName.Equals(providerName));

            return provider != null
                ? await provider.CreateReservationAsync(request)
                : null;
        }
    }
}
