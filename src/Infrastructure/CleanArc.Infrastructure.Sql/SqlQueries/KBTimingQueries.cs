using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class KBTimingQueries
{
    public static string Create_KB_RelatedUrlLink => "KnowledgeBase.Create_KBtiming";
    public static string update_KB_RelatedUrlLink => "KnowledgeBase.update_KBtiming";
    public static string Delete_KB_RelatedUrlLink => "KnowledgeBase.Delete_KBtiming";
    public static string usp_GetAll_KB_RelatedUrlLink => "KnowledgeBase.GetAll_KBtiming";
    public static string usp_GetByID_KB_RelatedUrlLink => "KnowledgeBase.GetByID_KBtiming";


}
