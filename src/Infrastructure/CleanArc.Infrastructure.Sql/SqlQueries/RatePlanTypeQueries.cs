using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class RatePlanTypeQueries
    {
        public static string Create_RatePlanType => "usp_Create_RatePlanType";
        public static string Update_RatePlanType => "usp_Update_RatePlanType";
        public static string Delete_RatePlanType => "usp_Delete_RatePlanType";
        public static string GetALL_RatePlanType => "usp_GetALL_RatePlanType";
        public static string GetByID_RatePlanType => "usp_GetByID_RatePlanType";
    }
}
