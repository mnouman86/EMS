using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class HotelImageQueries
    {
        public static string Create_HotelImage => "usp_Create_GenericMedia";
        public static string Update_HotelImage => "usp_Update_Media";
        public static string Delete_HotelImage => "usp_Delete_Media";
        public static string GetByID_HotelImage => "usp_GetByID_Media";
    }
}
