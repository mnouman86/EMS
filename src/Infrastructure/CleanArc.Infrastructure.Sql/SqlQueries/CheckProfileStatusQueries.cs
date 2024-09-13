using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class CheckProfileStatusQueries
{
    public static string Create_CheckProfileStatus => "MappingKB.Create_CheckProfileStatus";
    public static string update_CheckProfileStatus => "MappingKB.Update_CheckProfileStatus";
    public static string Delete_CheckProfileStatus => "MappingKB.Delete_CheckProfileStatus";
    public static string GetAll_CheckProfileStatus => "MappingKB.GetAll_CheckProfileStatus";
    public static string GetByID_ProfileStatusCheck => "GetByID_ProfileStatusCheck";


}
