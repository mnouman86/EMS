using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class ActivityAddressQueries
{
    public static string Create_ActivityAddress => "usp_Create_GenericAddress";
    public static string Update_ActivityAddress => "usp_Update_GenericAddress";
    public static string Delete_ActivityAddress => "usp_Delete_GenericAddress";
    public static string GetAll_ActivityAddress => "usp_GetAll_GenericAddress";
    public static string GetByID_ActivityAddress => "usp_GetByID_GenericAddress";


}
