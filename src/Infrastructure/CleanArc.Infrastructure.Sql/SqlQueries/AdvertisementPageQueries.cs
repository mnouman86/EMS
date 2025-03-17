using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class AdvertisementPageQueries
{
    public static string Create_Page => "usp_Create_AdsPage";
    public static string Update_Page => "usp_Update_AdsPage";
    public static string Delete_Page => "usp_Delete_AdsPage";
    public static string GetALL_Page => "usp_GetAll_AdsPage";
    public static string GetByID_Page => "usp_GetByID_AdsPage";


}
