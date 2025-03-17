using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class CampaignScheduleQueries
{
    public static string Create_CampaignSchedule => "usp_Create_CampaignSchedule";
    public static string Update_CampaignSchedule => "usp_Update_CampaignSchedule";
    public static string Delete_CampaignSchedule => "usp_Delete_CampaignSchedule";
    public static string GetALL_CampaignSchedule => "usp_GetALL_CampaignSchedule";
    public static string GetByID_CampaignSchedule => "usp_GetByID_CampaignSchedule";

}
