using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CleanArc.Infrastructure.Sql.SqlQueries;
public static class ActivitySeasonQueries
{
    public static string Create_ActivitySeason => "usp_Create_Seasons";
    public static string Update_Seasons => "usp_Update_Seasons";
    public static string Delete_Seasons => "usp_Delete_Seasons";
    public static string GetAll_Seasons => "usp_GetAll_Seasons";
    public static string GetByID_Seasons => "usp_GetByID_Seasons";
}
