using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class RoomDetailQueries
{
    public static string Create_RoomDetail => "usp_Create_RoomDetail";
    public static string Update_RoomDetail => "usp_Update_RoomDetail";
    public static string Delete_RoomDetail => "usp_Delete_RoomDetail";
    public static string GetALL_RoomDetail => "usp_GetALL_RoomDetail";
    public static string GetByID_RoomDetail => "usp_GetByID_RoomDetail";
    public static string GetHotelDetail_ByRoom => "usp_GetHotelDetail_ByRoom";

}
