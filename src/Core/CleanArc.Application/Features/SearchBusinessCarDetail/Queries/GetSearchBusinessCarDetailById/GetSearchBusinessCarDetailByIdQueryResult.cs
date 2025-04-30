using CleanArc.Application.Features.AmenityMapping.Queries.GetAllAmenityMapping;
using CleanArc.Application.Features.CarDetail.Queries.GetCarDetailById;
using CleanArc.Application.Features.CustomerReview.Queries.GetAllCustomerReviews;
using CleanArc.Application.Features.FAQs.Queries.GetAllFAQs;
using CleanArc.Application.Features.GenericMedia.Queries.GetAllGenericMedia;
using CleanArc.Application.Features.Language.Queries.GetAllLanguages;
using CleanArc.Application.Features.RoomDetails.Queries.GetAllRoomDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.SearchBusinessCarDetail.Queries.GetSearchBusinessCarDetailById;

public class GetSearchBusinessCarDetailByIdQueryResult : GetCarDetailByIdQueryResult
{
    public IEnumerable<GetCarDetailByIdQueryResult> Cars { get; set; }

    //public IEnumerable<GetAllAmenityMappingQueryResult> Amenities { get; set; }
    public IEnumerable<GetAllFAQsQueryResult> FAQs { get; set; }
    //public IEnumerable<GetAllLanguagesQueryResult> Languages { get; set; }
    public IEnumerable<GetAllCustomerReviewsQueryResult> Reviews { get; set; }
}
