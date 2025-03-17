using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class StateQueries
    {
        public static string Create_State => "usp_Create_State";
        public static string Update_State => "usp_Update_State";
        public static string Delete_State => "usp_Delete_State";
        public static string GetALL_State => "usp_GetALL_State";
        public static string GetByID_State => "usp_GetByID_State";
    }
}

