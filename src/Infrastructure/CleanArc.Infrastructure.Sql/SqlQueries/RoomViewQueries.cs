using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class RoomViewQueries
    {
        public static string Create_RoomView => "usp_Create_RoomView";
        public static string Update_RoomView => "usp_Update_RoomView";
        public static string Delete_RoomView => "usp_Delete_RoomView";
        public static string GetALL_RoomView => "usp_GetALL_RoomView";
        public static string GetByID_RoomView => "usp_GetByID_RoomView";
    }
}
