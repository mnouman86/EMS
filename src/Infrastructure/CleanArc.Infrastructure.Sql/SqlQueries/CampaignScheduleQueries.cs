using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class CampaignScheduleQueries
{
    public static string Create_CampaignSchedule => "Create_CampaignSchedule";
    public static string Update_CampaignSchedule => "Update_CampaignSchedule";
    public static string Delete_CampaignSchedule => "Delete_CampaignSchedule";
    public static string usp_GetALL_CampaignSchedule => "GetALL_CampaignSchedule";
    public static string usp_GetByID_CampaignSchedule => "GetByID_CampaignSchedule";

}
