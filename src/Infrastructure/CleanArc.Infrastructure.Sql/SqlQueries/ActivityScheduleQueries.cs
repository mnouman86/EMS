using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class ActivityScheduleQueries
{
    public static string Create_ActivitySchedule => "usp_Create_Schedule";
    public static string Update_ActivitySchedule => "usp_update_Schedule";
    public static string Delete_ActivitySchedule => "usp_Delete_Schedule";
    public static string GetAll_ActivitySchedule => "usp_GetAll_Schedule";
    public static string GetByID_ActivitySchedule => "usp_GetByID_Schedule";


}
