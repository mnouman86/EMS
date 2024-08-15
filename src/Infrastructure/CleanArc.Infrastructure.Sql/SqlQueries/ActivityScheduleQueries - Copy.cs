using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class ActivityScheduleQueries
{
    public static string Create_ActivitySchedule => "Create_Schedule";
    public static string update_ActivitySchedule => "update_Schedule";
    public static string Delete_ActivitySchedule => "Delete_Schedule";
    public static string usp_GetAll_ActivitySchedule => "GetAll_Schedule";
    public static string usp_GetByID_ActivitySchedule => "GetByID_Schedule";


}
