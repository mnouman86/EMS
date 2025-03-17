using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class ActivityTransportationQueries
{
    public static string Create_ActivityTransportation => "usp_Create_Transportation";
    public static string Update_ActivityTransportation => "usp_update_Transportation";
    public static string Delete_ActivityTransportation => "usp_Delete_Transportation";
    public static string GetAll_Transportation => "usp_GetAll_Transportation";
    public static string GetByID_Transportation => "usp_GetByID_Transportation";


}
