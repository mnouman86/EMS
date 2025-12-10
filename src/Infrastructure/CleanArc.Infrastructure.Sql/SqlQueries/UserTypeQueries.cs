using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class UserTypeQueries
{
    public static string Create_UserType => "usp_Create_UserType";
    public static string Update_UserType => "usp_Update_UserType";
    public static string Delete_UserType => "usp_Delete_UserType";
    public static string GetALL_UserType => "usp_GetALL_UserType";
    public static string GetByID_UserType => "usp_GetByID_UserType";

}
