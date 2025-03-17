using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class LanguageQueries
    {
        public static string Create_Language => "usp_Create_Language";
        public static string Update_Language => "usp_Update_Language";
        public static string Delete_Language => "usp_Delete_Language";
        public static string GetALL_Language => "usp_GetALL_Language";
        public static string GetByID_Language => "usp_GetByID_Language";
    }
}
