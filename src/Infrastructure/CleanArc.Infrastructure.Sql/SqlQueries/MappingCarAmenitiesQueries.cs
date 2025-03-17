using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class MappingCarAmenitiesQueries
    {
        public static string Create_Mapping_CarAmenities => "usp_Create_CarAmenityMapping";
        public static string Update_CarAmenityMapping => "usp_Update_CarAmenityMapping";
    }
}
