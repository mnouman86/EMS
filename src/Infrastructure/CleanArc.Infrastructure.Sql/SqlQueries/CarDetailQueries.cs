using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class CarDetailQueries
    {
        public static string Create_CarDetail => "usp_Create_CarDetail";
        public static string Update_CarDetail => "usp_Update_CarDetail";
        public static string Delete_CarDetail => "usp_Delete_CarDetail";
        public static string GetALL_CarDetail => "usp_GetALL_CarDetail";
        public static string GetByID_CarDetail => "usp_GetByID_CarDetail";
        public static string GetByID_CarDetailByBusiness => "usp_GetByID_CarDetailByBusiness";
    }
}
