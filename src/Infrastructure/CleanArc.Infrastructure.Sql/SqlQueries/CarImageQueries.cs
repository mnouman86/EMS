using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class CarImageQueries
    {
        public static string Create_CarImage => "usp_Create_CarImage";
        public static string Update_CarImage => "usp_Update_CarImage";
        public static string Delete_CarImage => "usp_Delete_CarImage";
        public static string GetByID_CarImage => "usp_GetByID_CarImage";
    }
}
