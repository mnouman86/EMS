using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class ActivityNatureQueries
{
    public static string Create_ActivityNature => "usp_Create_ActivityNature";
    public static string Update_ActivityNature => "usp_Update_ActivityNature";
    public static string Delete_ActivityNature => "usp_Delete_ActivityNature";
    public static string GetAll_Natures => "usp_GetAll_ActivityNature";
    public static string GetByID_Natures => "usp_GetByID_ActivityNature";


}
