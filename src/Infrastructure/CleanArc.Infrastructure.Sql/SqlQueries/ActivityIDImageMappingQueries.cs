using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class ActivityIDImageMappingQueries
{
    public static string Mapping_Create_Activity_Image => "Mapping_Create_Activity_Image";
    public static string Mapping_Update_ActivityAddress => "Mapping_Update_ActivityAddress";
    public static string Mapping_Delete_Activity_Image => "Mapping_Delete_Activity_Image";
    public static string Mapping_GetAll_Activity_Image => "Mapping_GetAll_Activity_Image";
    public static string Mapping_GetByActivityID_Activity_Image => "usp_GetByGenericID_ActivityImageMapping";


}
