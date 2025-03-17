using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class ActivityIncludeOptionMappingQueries
{
    public static string Mapping_Create_Seasons => "Create_Mapping_IncludeOptions";
    public static string Mapping_Update_Seasons => "Mapping_Update_Seasons";
    public static string Mapping_Delete_Seasons => "Mapping_Delete_Seasons";
    public static string Mapping_GetAllByActivityID_IncludeOptions => "usp_GetAllByGenericTitleID_IncludeOptionsMapping";
    public static string Mapping_GetByID_Seasons => "Mapping_GetByID_Seasons";

}
