using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class RoomDiscountByUserTypeQueries
{
    public static string Create_RoomDiscountByUserType => "usp_Create_RoomDiscountByUserType";
    public static string Update_RoomDiscountByUserType => "usp_Update_RoomDiscountByUserType";
    public static string Delete_RoomDiscountByUserType => "usp_Delete_RoomDiscountByUserType";
    public static string GetALL_RoomDiscountByUserType => "usp_GetALL_RoomDiscountByUserType";
    public static string GetByID_RoomDiscountByUserType => "usp_GetByID_RoomDiscountByUserType";

}
