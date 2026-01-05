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

namespace CleanArc.Application.Contracts.Providers
{
    //Interface for any hotel provider
    public interface IHotelProvider
    {
        APIProvider ProviderName { get; }
        Task<List<SearchDetail>> SearchHotelsAsync(CustomizedSearchRequest request);
        Task<HotelDetail> GetHotelDetailAsync(HotelDetailSearchRequest request);
        Task<BookingReservationResult> CreateReservationAsync(
        BookingReservationRequest request);
    }
}
