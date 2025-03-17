using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class ActivityPerGroupPriceQueries
{
    public static string Create_ActivityPerGroupPrice => "usp_Create_ActivityGroupWisePrice";
    public static string Update_ActivityPerGroupPrice => "usp_Update_ActivityGroupWisePrice";
    public static string Delete_ActivityPerGroupPrice => "usp_Delete_ActivityGroupWisePrice";
    public static string GetAll_ActivityPerGroupPrice => "usp_GetAll_ActivityGroupWisePrice";
    public static string GetByID_ActivityPerGroupPrice => "usp_GetByID_ActivityGroupWisePrice";
}
