using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.MappingCarAmenities
{
    public class MappingCarAmenities
    {
        public int? CarID { get; set; }
        public string? AmenitiesIDs { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
