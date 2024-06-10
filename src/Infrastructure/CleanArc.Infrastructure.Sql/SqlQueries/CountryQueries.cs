using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class CountryQueries
    {
        public static string Create_Country => "Create_Country";
        public static string Delete_Country => "Delete_Country";
        public static string Update_Country => "Update_Country";
        public static string usp_GetALL_Country => "usp_GetALL_Country";
        public static string usp_GetByID_Country => "usp_GetByID_Country";
    }
}
