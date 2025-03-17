using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class ActivityImageMappingQueries
{
    public static string Mapping_Create_Activity_Image => "usp_Create_ActivityImageMapping";
    public static string Mapping_Update_ActivityImage => "usp_Update_ActivityImageMapping";
    public static string Mapping_Delete_Activity_Image => "usp_Delete_ActivityImageMapping";
    public static string Mapping_GetAll_Activity_Image => "usp_GetAll_ActivityImageMapping";
    public static string Mapping_GetByID_Activity_Image => "usp_GetByID_ActivityImageMapping";


}
