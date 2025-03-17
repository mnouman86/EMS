using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public class CityQueries
    {
        public static string Create_City => "usp_Create_City";
        public static string Update_City => "usp_Update_City";
        public static string Delete_City => "usp_Delete_City";
        public static string GetAll_City => "usp_GetAll_City";
        public static string GetByID_City => "usp_GetByID_City";
    }
}
