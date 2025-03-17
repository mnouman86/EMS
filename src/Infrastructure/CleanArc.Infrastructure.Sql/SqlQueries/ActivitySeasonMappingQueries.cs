using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class ActivitySeasonMappingQueries
{
    public static string Mapping_Create_Seasons => "usp_Create_SeasonMapping";
    public static string Mapping_Update_Seasons => "usp_Update_SeasonMapping";
    public static string Mapping_Delete_Seasons => "usp_Delete_SeasonMapping";
    public static string Mapping_GetAll_Seasons => "usp_GetAll_SeasonMapping";
    public static string Mapping_GetByID_Seasons => "usp_GetByID_SeasonMapping";


}
