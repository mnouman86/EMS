using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class KBRelatedUrlLinkQueries
{
    public static string Create_KB_RelatedUrlLink => "KnowledgeBase.Create_KBRelatedUrlLink";
    public static string update_KB_RelatedUrlLink => "KnowledgeBase.update_KBRelatedUrlLink";
    public static string Delete_KB_RelatedUrlLink => "KnowledgeBase.Delete_KBRelatedUrlLink";
    public static string usp_GetAll_KB_RelatedUrlLink => "KnowledgeBase.GetAll_KBRelatedUrlLink";
    public static string usp_GetByID_KB_RelatedUrlLink => "KnowledgeBase.GetByID_KBRelatedUrlLink";


}
