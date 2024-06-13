using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.MappingCarAmenities
{
    public class CreateMappingCarAmenities
    {
        public string? CarID { get; set; }
        public string? AmenitiesIDs { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
