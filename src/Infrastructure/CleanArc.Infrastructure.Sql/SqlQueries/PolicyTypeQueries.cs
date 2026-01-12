using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class PolicyTypeQueries
    {
        public static string Create_PolicyType => "usp_Create_RefundPolicyType";
        public static string Update_PolicyType => "usp_Update_RefundPolicyType";
        public static string Delete_PolicyType => "usp_Delete_RefundPolicyType";
        public static string GetALL_PolicyType => "usp_GetALL_RefundPolicyType";
        public static string GetByID_PolicyType => "usp_GetByID_RefundPolicyType";
    }
}
