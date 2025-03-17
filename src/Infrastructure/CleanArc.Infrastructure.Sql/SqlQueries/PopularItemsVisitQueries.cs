using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class PopularItemsVisitQueries
{
    public static string Create_PopularItemsVisit => "KnowledgeBase.Create_PopularItemsVisits";
    public static string Update_PopularItemsVisit => "KnowledgeBase.Update_PopularItemsVisits";
    public static string Delete_PopularItemsVisit => "KnowledgeBase.Delete_PopularItemsVisits";
    public static string GetAll_PopularItemsVisit => "dbo.GetAll_PopularItemsVisits";
    public static string GetByID_PopularItemsVisit => "KnowledgeBase.GetByID_PopularItemsVisits";


}
