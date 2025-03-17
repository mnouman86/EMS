using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CleanArc.Infrastructure.Sql.SqlQueries;
public static class AgeTypeQueries
{
    public static string Create_AgeType => "usp_Create_AgeType";
    public static string Update_AgeType => "usp_update_AgeType";
    public static string Delete_AgeType => "usp_Delete_AgeType";
    public static string GetAll_AgeType => "usp_GetAll_AgeType";
    public static string GetByID_AgeType => "usp_GetByID_AgeType";
}
