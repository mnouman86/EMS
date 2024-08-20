using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.SearchHotelDetail
{
    public class SearchHotelDetail
    {
        public int ID { get; set; }
        public int? HotelID { get; set; }
        public int? CityID { get; set; }
        public String? HotelName { get; set; }
        public String? CityName { get; set; }
        public String? CityDescription { get; set; }
        public String? RoomTypeName { get; set; }
        public String? RoomTypeDescription { get; set; }
        public Decimal? RoomDetailPrice { get; set; }

        public Decimal? DiscountPercentage { get; set; }
        public Decimal? DiscountAmount { get; set; }
        public Decimal? DiscountedPrice { get; set; }
        public String? ImagePath { get; set; }
        public String? ImageTitle { get; set; }
        public String? Description { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
        public int? TotalDays { get; set; }
        public List<HotelImage> HotelImages { get; set; }


}
    public class HotelImage
    {
        public string ImageTitle { get; set; }
        public string ImagePath { get; set; }
        public bool IsMain { get; set; }
    }
}
