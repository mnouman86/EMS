using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.AmenityMapping
{
    public class AmenityMapping
    {
        public int? GenericTitleId { get; set; }
        public int[]? AmenitiesIDs { get; set; }
        public int? CultureId { get; set; }
        public int? ServiceTypeEnumId { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int Id { get; set; }
        public string? Amenity { get; set; }
        public string? Icon { get; set; }
        public bool? Selected { get; set; }
    }
}
