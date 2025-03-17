using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class RoomSizeUnitQueries
{
public static string Create_RoomSizeUnit => "usp_Create_RoomSizeUnit";
    public static string Update_RoomSizeUnit => "usp_Update_RoomSizeUnit";
    public static string Delete_RoomSizeUnit => "usp_Delete_RoomSizeUnit";
    public static string GetALL_RoomSizeUnit => "usp_GetALL_RoomSizeUnit";
    public static string GetByID_RoomSizeUnit => "usp_GetByID_RoomSizeUnit";
}
