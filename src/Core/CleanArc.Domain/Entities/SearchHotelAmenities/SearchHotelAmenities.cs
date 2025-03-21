using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.SearchHotelAmenities
{
    public class SearchHotelAmenities
    {
        public int? Id { get; set; }
        public int? HotelID { get; set; }

        public string? Amenity { get; set; }
        public string? Description { get; set; }
        public bool? Selected { get; set; }

    }
}
