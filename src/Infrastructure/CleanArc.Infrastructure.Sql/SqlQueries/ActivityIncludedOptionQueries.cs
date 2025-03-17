using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class ActivityIncludedOptionQueries
{
    public static string Create_ActivityIncludedOption => "usp_Create_IncludeOption";
    public static string Update_ActivityIncludedOption => "usp_Update_IncludeOption";
    public static string Delete_ActivityIncludedOption => "usp_Delete_IncludeOption";
    public static string LookUp_GetAll_IncludeOptions => "usp_GetALL_IncludeOption";
    public static string Mapping_GetByID_IncludeOptions => "usp_GetByID_IncludeOption";


}
