using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class RoomVisualQueries
    {
        public static string Creat_RoomImage => "usp_Create_RoomImage";
        public static string Delete_RoomImage => "usp_Delete_RoomImage";
        public static string GetByID_RoomImage => "usp_GetByID_RoomImage";
        public static string Update_RoomImage => "usp_Update_RoomImage";
    }
}
