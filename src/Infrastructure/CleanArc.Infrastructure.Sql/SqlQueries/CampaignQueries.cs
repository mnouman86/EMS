using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class CampaignQueries
{
    public static string Create_Campaign => "usp_Create_Campaign";
    public static string Update_Campaign => "usp_Update_Campaign";
    public static string Delete_Campaign => "usp_Delete_Campaign";
    public static string GetALL_Campaign => "usp_GetAll_Campaign";
    public static string GetByID_Campaign => "usp_GetByID_Campaign";

}
