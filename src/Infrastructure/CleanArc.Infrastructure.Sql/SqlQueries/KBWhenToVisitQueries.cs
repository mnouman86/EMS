using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class KBWhenToVisitQueries
{
    public static string Create_WhenToVisit => "LookUpKB.Create_WhenToVisite";
    public static string Update_WhenToVisit => "LookUpKB.update_WhenToVisite";
    public static string Delete_WhenToVisit => "LookUpKB.Delete_WhenToVisite";
    public static string GetAll_WhenToVisit => "LookUpKB.GetAll_WhenToVisite";
    public static string GetByID_WhenToVisit => "LookUpKB.GetByID_WhenToVisite";


}
