using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class CurrencyQueries
{
    public static string Create_Currency => "usp_Create_Currency";
    public static string Update_Currency => "usp_update_Currency";
    public static string Delete_Currency => "usp_Delete_Currency";
    public static string GetAll_Currency => "usp_GetAll_Currency";
    public static string GetByID_Currency => "usp_GetByID_Currency";


}
