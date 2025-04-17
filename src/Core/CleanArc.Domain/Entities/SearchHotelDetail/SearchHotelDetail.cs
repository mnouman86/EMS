using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.SearchHotelDetail
{
    public class SearchHotelDetail
    {
        public List<SearchDetail> HotelDetail { get; set; }
        public Decimal? RoomPriceMinimum { get; set; }
        public Decimal? RoomPriceMaximum { get; set; }
    }

        public class SearchDetail
    {
        public int Id { get; set; }
        public int? HotelID { get; set; }
        public int? CityID { get; set; }
        public String? Name { get; set; }
        public String? CityName { get; set; }
        public String? CityDescription { get; set; }
        public String? RoomTypeName { get; set; }
        public String? RoomTypeDescription { get; set; }
        
       

        public Decimal? DiscountPercentage { get; set; }
        public Decimal? DiscountAmount { get; set; }
        public Decimal? DiscountedPrice { get; set; }
        public String? ImagePath { get; set; }
        public String? ImageTitle { get; set; }
        public String? Description { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
        public int? TotalDays { get; set; }
        public Decimal? TotalPrice { get; set; }
        public Decimal? RoomDetailPrice { get; set; }
        public int? ReviewsCount { get; set; }
        public int? Rating { get; set; }
        public string? RefundPolicy { get; set; }
        public List<GenericMedia.GenericMedia> HotelImages { get; set; }
        public List<AmenityMapping.AmenityMapping> Amenities { get; set; }


}
    //public class HotelImage
    //{
    //    public string ImageTitle { get; set; }
    //    public string ImagePath { get; set; }
    //    public bool IsMain { get; set; }
    //}
}
