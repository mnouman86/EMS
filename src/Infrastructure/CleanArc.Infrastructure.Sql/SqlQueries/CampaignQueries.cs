using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class CampaignQueries
{
    public static string Create_Campaign => "Create_Campaign";
    public static string Update_Campaign => "Update_Campaign";
    public static string Delete_Campaign => "Delete_Campaign";
    public static string usp_GetALL_Campaign => "GetALL_Campaign";
    public static string usp_GetByID_Campaign => "GetByID_Campaign";

}
