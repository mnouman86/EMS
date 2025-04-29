using CleanArc.Application.Features.AmenityMapping.Queries.GetAllAmenityMapping;
using CleanArc.Application.Features.GenericMedia.Queries.GetAllGenericMedia;
using CleanArc.Application.Features.SearchHotelDetail.Queries.GetAllSearchHotelDetail;
using CleanArc.Domain.Entities.SearchBusinessCarDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.SearchBusinessCarDetail.Queries.GetAllSearchBusinessCarDetail
{
    public class GetAllSearchBusinessCarDetailQueryResult
    {
        public List<GetAllSearchCarDetail> CarDetail { get; set; }
        public Decimal? CarPriceMinimum { get; set; }
        public Decimal? CarPriceMaximum { get; set; }
    }
    public class GetAllSearchCarDetail
    {
        public int Id { get; set; }
        public int? ServiceTypeEnumId { get; set; }
        public int? ServiceCategoryId { get; set; }
        public int? BusinessId { get; set; }
        public string? Model { get; set; }
        public string? Year { get; set; }
        public string? VehicleIdentificationNumber { get; set; }
        public string? PlateNumber { get; set; }
        public int? NoOfSeat { get; set; }
        public int? Price { get; set; }
        public int? RentPrice { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CultureId { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? About { get; set; }
        public string? RefundPolicy { get; set; }
        public string? NonRefundPolicy { get; set; }
        public string? CancellationPolicy { get; set; }
        public int? VehicleTypeLookUpId { get; set; }
        public int? DrivingAvailabilityOptionLookUpId { get; set; }
        public decimal? PerHourPrice { get; set; }
        public string? BusinessName { get; set; }
        public int? NoOfDays { get; set; }
        public int CarId { get; set; }
        public int CityId { get; set; }
        public string Name { get; set; }
        public string CityName { get; set; }
        public string VehicleTypeName { get; set; }
        public decimal CarDetailPrice { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal DiscountedPrice { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
        public Decimal? TotalPrice { get; set; }
        public int? ReviewsCount { get; set; }
        public int? Rating { get; set; }

        //public List<CleanArc.Domain.Entities.SearchBusinessCarDetail.SearchCarImage> SearchCarImage { get; set; }
        //public List<CleanArc.Domain.Entities.SearchBusinessCarDetail.SearchCarAmenities> SearchCarAmenities { get; set; }
        public List<GetAllGenericMediaQueryResult> CarImages { get; set; }
        public List<GetAllAmenityMappingQueryResult> Amenities { get; set; }
    }
}
