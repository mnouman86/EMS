using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class PolicyQueries
    {
        public static string Create_Policy => "usp_Create_BusinessRefundPolicy";
        public static string Update_Policy => "usp_Update_BusinessRefundPolicy";
        public static string Delete_Policy => "usp_Delete_BusinessRefundPolicy";
        public static string GetALL_Policy => "usp_GetALL_BusinessRefundPolicy";
        public static string GetByID_Policy => "usp_GetByID_BusinessRefundPolicy";
    }
}
