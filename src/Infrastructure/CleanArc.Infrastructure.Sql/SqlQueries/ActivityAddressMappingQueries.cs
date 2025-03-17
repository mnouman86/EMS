using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class ActivityAddressMappingQueries
{
    public static string Mapping_Create_Activity_Image => "Mapping_Create_Activity_Image";
    public static string Mapping_Update_ActivityAddress => "Mapping_Update_ActivityAddress";
    public static string Mapping_Delete_Activity_Image => "Mapping_Delete_Activity_Image";
    public static string Mapping_GetAll_Activity_Image => "Mapping_GetAll_Activity_Image";
    public static string GetByActivityID_ActivityAddress => "usp_GetByGenericID_GenericAddress";


}
