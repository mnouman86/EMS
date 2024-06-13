using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.SearchRoomAmenities;

public class SearchRoomAmenities
{
    public string? Amenity { get; set; }
    public string? Description { get; set; }
    public bool Selected { get; set; }
    public int ID { get; set; }
    public int RoomID { get; set; }
}
