using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class ActivityDisabilityOptionQueries
{
    public static string Create_ActivityDisabilityOption => "usp_Create_DisabilityOption";
    public static string Update_ActivityDisabilityOption => "usp_update_DisabilityOption";
    public static string Delete_ActivityDisabilityOption => "usp_Delete_DisabilityOption";
    public static string GetAll_DisabilityOptions => "usp_GetAll_DisabilityOption";
    public static string GetByID_DisabilityOptions => "usp_GetByID_DisabilityOption";


}
