using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CleanArc.Infrastructure.Sql.SqlQueries;
public static class FeedbackSubjectTypeQueries
{
    public static string Create_FeedbackSubjectType => "usp_Create_FeedbackSubjectType";
    public static string Update_FeedbackSubjectType => "usp_update_FeedbackSubjectType";
    public static string Delete_FeedbackSubjectType => "usp_Delete_FeedbackSubjectType";
    public static string GetAll_FeedbackSubjectType => "usp_GetAll_FeedbackSubjectType";
    public static string GetByID_FeedbackSubjectType => "usp_GetByID_FeedbackSubjectType";
}
