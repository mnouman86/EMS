using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class ActivityTypeQueries
{
    public static string Create_ActivityType => "usp_Create_ActivityType";
    public static string Update_ActivityType => "usp_update_ActivityType";
    public static string Delete_ActivityType => "usp_Delete_ActivityType";
    public static string GetAll_ActivityType => "usp_GetAll_ActivityType";
    public static string GetByID_ActivityType => "usp_GetByID_ActivityType";


}
