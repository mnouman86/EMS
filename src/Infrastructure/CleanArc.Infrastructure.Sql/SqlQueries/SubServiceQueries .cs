using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class SubServiceQueries
{
    public static string Create_SubService => "usp_Create_SubServiceCategory";
    public static string Update_SubService => "usp_Update_SubServiceCategory";
    public static string Delete_SubService => "usp_Delete_SubServiceCategory";
    public static string GetAll_SubServices => "usp_GetAll_SubServiceCategory";
    public static string GetByID_SubServices => "usp_GetByID_SubServiceCategory";


}
