using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class NoShowPolicyQueries
    {
        public static string Create_NoShowPolicy => "usp_Create_BusinessNoShowPolicy";
        public static string Update_NoShowPolicy => "usp_Update_BusinessNoShowPolicy";
        public static string Delete_NoShowPolicy => "usp_Delete_BusinessNoShowPolicy";
        public static string GetALL_NoShowPolicy => "usp_GetALL_BusinessNoShowPolicy";
        public static string GetByID_NoShowPolicy => "usp_GetByID_BusinessNoShowPolicy";
    }
}
