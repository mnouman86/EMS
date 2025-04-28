using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class VehicleTypeQueries
    {
        public static string Create_VehicleType => "usp_Create_VehicleType";
        public static string Update_VehicleType => "usp_Update_VehicleType";
        public static string Delete_VehicleType => "usp_Delete_VehicleType";
        public static string GetALL_VehicleType => "usp_GetALL_VehicleType";
        public static string GetByID_VehicleType => "usp_GetByID_VehicleType";
    }
}
