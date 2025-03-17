using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class ActivityDisabilityMappingQueries
{
    public static string Mapping_Create_Disability => "usp_Create_DisabilityOptionsMapping";
    public static string Mapping_Update_Disability => "usp_Update_DisabilityOptionsMapping";
    public static string Mapping_Delete_Disability => "usp_Delete_DisabilityOptionMapping";
    public static string Mapping_GetAll_Disability => "usp_GetAll_DisabilityOptionMapping";
    public static string Mapping_GetByID_Disability => "usp_GetByID_DisabilityOptionMapping";


}
