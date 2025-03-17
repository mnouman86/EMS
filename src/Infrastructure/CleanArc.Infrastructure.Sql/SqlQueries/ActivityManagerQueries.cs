using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class ActivityManagerQueries
{
    public static string Create_ActivityManager => "usp_Create_ActivitySupervisor";
    public static string Update_ActivityManager => "usp_Update_ActivitySupervisor";
    public static string Delete_ActivityManager => "usp_Delete_ActivitySupervisor";
    public static string GetAll_Manager => "usp_GetAll_ActivitySupervisor";
    public static string GetByID_Manager => "usp_GetByID_ActivitySupervisor";


}
