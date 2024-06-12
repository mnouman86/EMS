using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.MappingHotelAmenities
{
    public class MappingHotelAmenities
    {
        public int? HotelID { get; set; }
        public string? AmenitiesIDs { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
