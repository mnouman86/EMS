using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class CustomerAwarenessQueries
{
    public static string Create_CustomerAwareness => "usp_Create_CustomerAwareness";
    public static string Update_CustomerAwareness => "usp_Update_CustomerAwareness";
    public static string Delete_CustomerAwareness => "usp_Delete_CustomerAwareness";
    public static string GetALL_CustomerAwareness => "usp_GetALL_CustomerAwareness";
    public static string GetByID_CustomerAwareness => "usp_GetByID_CustomerAwareness";

}
