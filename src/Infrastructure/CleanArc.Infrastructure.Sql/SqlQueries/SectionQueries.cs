using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class SectionQueries
    {
        public static string Create_Section => "Create_Section";
        public static string Delete_Section => "Delete_Section";
        public static string Update_Section => "Update_Section";
        public static string usp_GetALL_Sections => "usp_GetALL_Sections";
        public static string usp_GetByID_Section => "usp_GetByID_Section";
    }
}
