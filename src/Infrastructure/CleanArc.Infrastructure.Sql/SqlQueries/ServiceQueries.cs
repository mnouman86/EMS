using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class ServiceQueries
    {
        public static string Create_Service => "usp_Create_Service";
        public static string Update_Service => "usp_Update_Service";
        public static string Delete_Service => "usp_Delete_Service";
        public static string GetALL_Service => "usp_GetALL_Service";
        public static string GetByID_Service => "usp_GetByID_Service";
    }
}
