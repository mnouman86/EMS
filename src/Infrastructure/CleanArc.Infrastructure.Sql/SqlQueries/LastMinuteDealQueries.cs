using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class LastMinuteDealQueries
    {
        public static string Create_LastMinuteDeal => "usp_Create_LastMinuteDeal";
        public static string Update_LastMinuteDeal => "usp_Update_LastMinuteDeal";
        public static string Delete_LastMinuteDeal => "usp_Delete_LastMinuteDeal";
        public static string GetALL_LastMinuteDeals => "usp_GetALL_LastMinuteDeals";
        public static string GetByID_LastMinuteDeal => "usp_GetByID_LastMinuteDeal";
    }
}
