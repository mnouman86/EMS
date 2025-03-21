using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.SearchRoomAmenities.Queries.GetAllSearchRoomAmenities
{
    public class GetAllSearchRoomAmenitiesQueryResult
    {
        public string Amenity { get; set; }
        public string Description { get; set; }
        public bool Selected { get; set; }
        public int Id { get; set; }
        public int RoomID { get; set; }
    }
}
