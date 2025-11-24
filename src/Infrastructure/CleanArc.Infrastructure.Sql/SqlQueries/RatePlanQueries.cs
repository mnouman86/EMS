using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class RatePlanQueries
    {
        public static string Create_RatePlan => "usp_Create_RatePlan";
        public static string Update_RatePlan => "usp_Update_RatePlan";
        public static string Delete_RatePlan => "usp_Delete_RatePlan";
        public static string GetALL_RatePlan => "usp_GetALL_RatePlan";
        public static string GetByID_RatePlan => "usp_GetByID_RatePlan";
    }
}
