using CleanArc.Application.Features.AmenityMapping.Queries.GetAllAmenityMapping;
using CleanArc.Application.Features.CustomerReview.Queries.GetAllCustomerReviews;
using CleanArc.Application.Features.FAQs.Queries.GetAllFAQs;
using CleanArc.Application.Features.GenericMedia.Queries.GetAllGenericMedia;
using CleanArc.Application.Features.Hotel.Queries.GetHotelById;
using CleanArc.Application.Features.OutDoor.Queries.GetAllOutDoor;
using CleanArc.Application.Features.RoomDetails.Queries.GetAllRoomDetail;
using CleanArc.Application.Features.RoomView.Queries.GetAllRoomView;
using CleanArc.Domain.Entities.NearByLocation;
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
        public IEnumerable<GetAllAmenityMappingQueryResult> Amenities { get; set; }
        public IEnumerable<GetAllFAQsQueryResult> FAQs { get; set; }
        public IEnumerable<GetAllCustomerReviewsQueryResult> Reviews { get; set; }
        public IEnumerable<GetAllRoomViewQueryResult> RoomView { get; set; }
        public IEnumerable<GetAllOutDoorQueryResult> OutDoor { get; set; }
        public IEnumerable<NearByLocation> NearByLocations { get; set; }
    }
}
