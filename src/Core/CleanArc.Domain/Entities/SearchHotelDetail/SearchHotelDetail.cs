using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.SearchHotelDetail
{
    public class SearchHotelDetail
    {
        public int? HotelID { get; set; }
        public int? CityID { get; set; }
        public String? HotelName { get; set; }
        public String? CityName { get; set; }
        public String? CityDescription { get; set; }
        public String? RoomTypeName { get; set; }
        public String? RoomTypeDescription { get; set; }
        public Decimal? RoomDetailPrice { get; set; }




    }
}
