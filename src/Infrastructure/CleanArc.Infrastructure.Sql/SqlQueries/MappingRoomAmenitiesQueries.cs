using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class MappingRoomAmenitiesQueries
    {
        public static string Create_Mapping_RoomAmenities => "usp_Create_RoomAmenityMapping";
        public static string Update_Mapping_RoomAmenities => "usp_Update_RoomAmenityMapping";
    }
}
