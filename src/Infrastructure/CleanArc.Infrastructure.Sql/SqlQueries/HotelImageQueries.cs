using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class HotelImageQueries
    {
        public static string Create_HotelImage => "Create_HotelImage";
        public static string Update_HotelImage => "Update_Media";
        public static string Delete_HotelImage => "Delete_Media";
        //public static string usp_GetALL_CarDetail => "usp_GetALL_CarDetail";
        public static string usp_GetByID_HotelImage => "GetByID_Media";
    }
}
