using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.MappingHotelAmenities
{
    public class CreateMappingHotelAmenities
    {
        public string? HotelIDs { get; set; }
        public string? AmenitiesIDs { get; set; }
        public int? CreatedBy { get; set; }
    }
}
