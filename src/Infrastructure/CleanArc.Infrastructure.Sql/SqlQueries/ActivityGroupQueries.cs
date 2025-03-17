using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class ActivityGroupQueries
{
    public static string Create_Group => "usp_Create_GroupActivityMembers";
    public static string Update_Group => "usp_Update_GroupActivityMembers";
    public static string Delete_Group => "usp_Delete_GroupActivityMembers";
    public static string GetAll_Group => "usp_GetAll_GroupActivityMembers";
    public static string GetByID_Group => "usp_GetByID_GroupActivityMembers";


}
