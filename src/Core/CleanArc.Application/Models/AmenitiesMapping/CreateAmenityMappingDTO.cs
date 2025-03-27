using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.AmenitiesMapping
{
    public class CreateAmenityMappingDTO
    {
        //public string? HotelID { get; set; }
        //public string? AmenitiesIDs { get; set; }
        //public int? CreatedBy { get; set; }

        //public int? UpdatedBy { get; set; }
        public int? GenericTitleId { get; set; }
        //public int[]? AmenitiesIDs { get; set; }
        public int? CultureId { get; set; }
        public int? AmenityTypeEnumId { get; set; }
        public int? CreatedBy { get; set; }
    }
}
