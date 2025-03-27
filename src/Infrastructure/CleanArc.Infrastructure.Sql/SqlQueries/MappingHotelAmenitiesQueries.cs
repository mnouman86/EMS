using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class AmenityMappingQueries
    {
        public static string Create_Mapping_HotelAmenities => "usp_Create_AmenityMapping";
        public static string GetByAmenityTypeEnumID_AmenityMapping => "usp_GetByAmenityTypeEnumID_AmenityMapping";

    }
}
