using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class RoomRateQueries
{
    public static string Create_RoomRate => "usp_Create_RoomRate";
    public static string Update_RoomRate => "usp_Update_RoomRate";
    public static string Delete_RoomRate => "usp_Delete_RoomRate";
    public static string GetALL_RoomRate => "usp_GetALL_RoomRate";
    public static string GetByID_RoomRate => "usp_GetByID_RoomRate";

}
