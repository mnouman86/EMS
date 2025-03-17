using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class HotelQueries
{
    public static string Create_Hotel => "usp_Create_Hotel";
    public static string Update_Hotel => "usp_update_Hotel";
    public static string Delete_Hotel => "usp_Delete_Hotel";
    public static string GetALL_Hotel => "usp_GetALL_Hotel";
    public static string GetByID_Hotel => "usp_GetByID_Hotel";
}
