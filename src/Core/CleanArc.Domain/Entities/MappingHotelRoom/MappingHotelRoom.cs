using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.MappingHotelRoom
{
    public class MappingHotelRoom
    {
        public int? HotelID { get; set; }
        public string? RoomIDs { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
