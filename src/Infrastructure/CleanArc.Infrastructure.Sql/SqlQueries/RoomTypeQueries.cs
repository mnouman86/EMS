using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class RoomTypeQueries
{
    public static string Create_RoomType => "usp_Create_RoomType";
    public static string Update_RoomType => "usp_Update_RoomType";
    public static string Delete_RoomType => "usp_Delete_RoomType";
    public static string GetALL_RoomType => "usp_GetALL_RoomType";
    public static string GetByID_RoomType => "usp_GetByID_RoomType";

}
