using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.MappingRoomAmenities
{
    public class CreateMappingRoomAmenities
    {
        public int? RoomID { get; set; }
        public string? AmenitiesIDs { get; set; }
        public int? CategoryID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
