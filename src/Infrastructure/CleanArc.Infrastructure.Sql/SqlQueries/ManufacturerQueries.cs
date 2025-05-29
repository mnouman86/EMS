using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class ManufacturerQueries
    {
        public static string Create_Manufacturer => "usp_Create_Manufacturer";
        public static string Update_Manufacturer => "usp_Update_Manufacturer";
        public static string Delete_Manufacturer => "usp_Delete_Manufacturer";
        public static string GetALL_Manufacturer => "usp_GetALL_Manufacturer";
        public static string GetByID_Manufacturer => "usp_GetByID_Manufacturer";
    }
}
