using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CleanArc.Infrastructure.Sql.SqlQueries;
public static class GenderQueries
{
    public static string Create_Gender => "usp_Create_Gender";
    public static string Update_Gender => "usp_update_Gender";
    public static string Delete_Gender => "usp_Delete_Gender";
    public static string GetAll_Gender => "usp_GetAll_Gender";
    public static string GetByID_Gender => "usp_GetByID_Gender";
}
