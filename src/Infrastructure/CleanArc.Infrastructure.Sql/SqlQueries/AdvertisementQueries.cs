using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class AdvertisementQueries
{
    public static string Create_Ads => "usp_Create_Advertisement";
    public static string Delete_Ads => "usp_Delete_Advertisement";
    public static string Update_Ads => "usp_Update_Advertisement";
    public static string GetALL_Ads => "usp_GetAll_Advertisement";
    public static string GetByID_Ads => "usp_GetByID_Advertisement";
}
