using CleanArc.Application.Features.AmenityMapping.Queries.GetAllAmenityMapping;
using CleanArc.Application.Features.GenericMedia.Queries.GetAllGenericMedia;
using CleanArc.Application.Features.SearchHotelDetail.Queries.GetAllSearchHotelDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.LastMinuteDeal.Queries.GetAllLastMinuteDeal
{
    public class GetAllLastMinuteDeals
    {
        //public List<GetAllLastMinuteDeals> HotelDetail { get; set; }
        public Decimal? RoomPriceMinimum { get; set; }
        public Decimal? RoomPriceMaximum { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
    public class GetAllLastMinuteDealQueryResult
    {
        public int? NoOfRooms { get; set; }
        public int? NoOfDays { get; set; }
        public int Id { get; set; }
        public int GenericTitleId { get; set; }
        public int HotelID { get; set; }
        public int CityID { get; set; }
        public string Name { get; set; }
        public string CityName { get; set; }
        public string CityDescription { get; set; }
        public string RoomTypeName { get; set; }
        public string RoomTypeDescription { get; set; }
        public decimal RoomDetailPrice { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal DiscountedPrice { get; set; }
        public string ImagePath { get; set; }
        public string ImageTitle { get; set; }

        public bool? IsInWishlist { get; set; }

        public int TotalDays { get; set; }
        public string? Description { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
        public Decimal? TotalPrice { get; set; }
        public int? ReviewsCount { get; set; }
        public int? Rating { get; set; }
        public string? RefundPolicy { get; set; }
        public List<GetAllGenericMediaQueryResult> HotelImages { get; set; }
        public List<GetAllAmenityMappingQueryResult> Amenities { get; set; }
        public decimal? Discount { get; set; }
        public int? FilterCategoryLookUpId { get; set; }
        public string? FilterCategoryName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        //public int Id { get; set; }
        //public int ServiceTypeEnumId { get; set; }
        //public int GenericTitleId { get; set; }
        //public int FilterCategoryLookUpId { get; set; }
        //public decimal? Discount { get; set; }
        //public DateTime? StartDate { get; set; }
        //public DateTime? EndDate { get; set; }
        //public string? Priority { get; set; }
        //public bool IsActive { get; set; }
        //public bool IsDeleted { get; set; }
        //public int CreatedBy { get; set; }
        //public DateTime CreatedAt { get; set; }
        //public int UpdatedBy { get; set; }
        //public DateTime UpdatedAt { get; set; }
        //public int LastMinuteDealLookUpId { get; set; }



    }
}

