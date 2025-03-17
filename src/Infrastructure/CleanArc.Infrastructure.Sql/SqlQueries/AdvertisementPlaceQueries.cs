using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class AdvertisementPlaceQueries
{
    public static string Create_Place => "usp_Create_AdsPlace";
    public static string Update_Place => "usp_Update_AdsPlace";
    public static string Delete_Place => "usp_Delete_AdsPlace";
    public static string GetALL_Place => "usp_GetAll_AdsPlace";
    public static string GetByID_Place => "usp_GetByID_AdsPlace";


}
