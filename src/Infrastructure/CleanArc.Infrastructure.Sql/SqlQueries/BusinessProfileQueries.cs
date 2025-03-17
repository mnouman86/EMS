using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class BusinessProfileQueries
    {
        public static string Create_BusinessProfile => "Create_BusinessProfile";
        public static string Update_BusinessProfile => "Update_BusinessProfile";
        public static string Delete_BusinessProfile => "Delete_BusinessProfile";
        public static string GetALl_BusinessProfile => "usp_GetALl_BusinessProfile";
        public static string GetByID_BusinessProfile => "usp_GetByID_BusinessProfile";
    }
}
