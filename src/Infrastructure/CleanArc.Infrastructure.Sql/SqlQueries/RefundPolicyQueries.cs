using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class RefundPolicyQueries
    {
        public static string Create_RefundPolicy => "usp_Create_RefundPolicy";
        public static string Update_RefundPolicy => "usp_Update_RefundPolicy";
        public static string Delete_RefundPolicy => "usp_Delete_RefundPolicy";
        public static string GetALL_RefundPolicy => "usp_GetALL_RefundPolicy";
        public static string GetByID_RefundPolicy => "usp_GetByID_RefundPolicy";
    }
}
