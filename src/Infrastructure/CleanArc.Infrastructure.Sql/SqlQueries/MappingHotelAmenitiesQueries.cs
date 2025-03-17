using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class MappingHotelAmenitiesQueries
    {
        public static string Create_Mapping_HotelAmenities => "usp_Create_HotelAmenityMapping";
        public static string Update_HotelAmenityMapping => "usp_Update_HotelAmenityMapping";
    }
}
