using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.MappingHotelRoom;

public class CreateMappingHotelRoom
{
    public int? HotelID { get; set; }
    public string? RoomIDs { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }
}


