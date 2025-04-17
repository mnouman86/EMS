using CleanArc.Application.Features.AmenityMapping.Queries.GetAllAmenityMapping;
using CleanArc.Application.Features.CustomerReview.Queries.GetAllCustomerReviews;
using CleanArc.Application.Features.FAQs.Queries.GetAllFAQs;
using CleanArc.Application.Features.GenericMedia.Queries.GetAllGenericMedia;
using CleanArc.Application.Features.Hotel.Queries.GetHotelById;
using CleanArc.Application.Features.RoomDetails.Queries.GetAllRoomDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.RoomDetails.Queries.GetHotelDetailForRoom
{
    public class GetHotelDetailForRoomQueryResult:GetHotelByIdQueryResult
    //(int Id, string Name, string Description, bool IsDeleted, bool IsActive, int CreatedBy, DateTime CreatedAt, int UpdatedBy, DateTime UpdatedAt);
    {
        public IEnumerable<GetAllRoomDetailQueryResult> Rooms { get; set; }
        public IEnumerable<GetAllGenericMediaQueryResult> Medias { get; set; }
        public IEnumerable<GetAllAmenityMappingQueryResult> HotelAmenities { get; set; }
        public IEnumerable<GetAllFAQsQueryResult> FAQs { get; set; }
        public IEnumerable<GetAllCustomerReviewsQueryResult> Reviews { get; set; }
    }
}
