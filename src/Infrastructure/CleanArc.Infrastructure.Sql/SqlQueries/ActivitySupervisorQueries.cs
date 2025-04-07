using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class ActivitySupervisorQueries
{
    public static string Create_ActivitySupervisor => "usp_Create_ActivitySupervisor";
    public static string Update_ActivitySupervisor => "usp_Update_ActivitySupervisor";
    public static string Delete_ActivitySupervisor => "usp_Delete_ActivitySupervisor";
    public static string GetAll_Manager => "usp_GetAll_ActivitySupervisor";
    public static string GetByID_Manager => "usp_GetByID_ActivitySupervisor";


}
