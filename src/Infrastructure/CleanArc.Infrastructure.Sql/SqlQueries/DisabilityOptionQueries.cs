using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class DisabilityOptionQueries
{
    public static string Create_DisabilityOption => "usp_Create_DisabilityOption";
    public static string Update_DisabilityOption => "usp_Update_DisabilityOption";
    public static string Delete_DisabilityOption => "usp_Delete_DisabilityOption";
    public static string GetAll_DisabilityOption => "usp_GetAll_DisabilityOption";
    public static string GetByID_DisabilityOption => "usp_GetByID_DisabilityOption";


}
