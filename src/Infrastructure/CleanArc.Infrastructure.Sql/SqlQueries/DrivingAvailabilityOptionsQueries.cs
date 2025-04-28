using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class DrivingAvailabilityOptionsQueries
    {
        public static string Create_DrivingAvailabilityOptions => "usp_Create_DrivingAvailabilityOption";
        public static string Update_DrivingAvailabilityOptions => "usp_Update_DrivingAvailabilityOption";
        public static string Delete_DrivingAvailabilityOptions => "usp_Delete_DrivingAvailabilityOption";
        public static string GetALL_DrivingAvailabilityOptions => "usp_GetALL_DrivingAvailabilityOption";
        public static string GetByID_DrivingAvailabilityOptions => "usp_GetByID_DrivingAvailabilityOption";
    }
}
