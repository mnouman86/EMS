using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class ActivityQueries
{
    public static string Create_Activity => "[dbo].[usp_Create_Activity]";
    public static string Update_Activity => "[dbo].[usp_Update_Activity]";
    public static string Delete_Activity => "[dbo].[usp_Delete_Activity]";
    public static string GetAll_Activity => "[dbo].[usp_GetAll_Activity]";
    public static string GetAllByBusinessID_Activities => "[dbo].[usp_GetAllByBusinessID_Activity]";
    public static string GetByID_Activity => "[dbo].[usp_GetByID_Activity]";
    public static string GetByID_ActivityDetailByBusiness => "usp_GetByID_ActivityDetailByBusiness";


}
