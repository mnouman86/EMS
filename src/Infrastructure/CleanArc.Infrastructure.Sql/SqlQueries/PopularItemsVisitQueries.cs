using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class PopularItemsVisitQueries
{
    public static string Create_PopularItemsVisit => "KnowledgeBase.usp_Create_PopularItemsVisits";
    public static string Update_PopularItemsVisit => "KnowledgeBase.usp_Update_PopularItemsVisits";
    public static string Delete_PopularItemsVisit => "KnowledgeBase.usp_Delete_PopularItemsVisits";
    public static string GetAll_PopularItemsVisit => "dbo.usp_GetAll_PopularItemVisit";
    public static string GetAll_PopularItemsCityWise => "dbo.usp_GetAll_PopularItemsCityWise";
    public static string GetByID_PopularItemsVisit => "KnowledgeBase.usp_GetByID_PopularItemsVisits";


}
