using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class SearchRoomAmenitiesQueries
    {
        public static string GetByHotelID_RoomAmenities => "usp_GetByHotelID_RoomAmenities";
        
    }
}
