using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class HotelQueries
{
    public static string Create_Hotel => "Create_Hotel";
    public static string update_Hotel => "update_Hotel";
    public static string Delete_Hotel => "Delete_Hotel";
    public static string usp_GetALL_Hotel => "usp_GetALL_Hotel";
    public static string usp_GetByID_Hotel => "usp_GetByID_Hotel";
}
