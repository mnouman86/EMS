using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CleanArc.Infrastructure.Sql.SqlQueries;
public static class FeedbackStatusQueries
{
    public static string Create_FeedbackStatus => "usp_Create_FeedbackStatus";
    public static string Update_FeedbackStatus => "usp_update_FeedbackStatus";
    public static string Delete_FeedbackStatus => "usp_Delete_FeedbackStatus";
    public static string GetAll_FeedbackStatus => "usp_GetAll_FeedbackStatus";
    public static string GetByID_FeedbackStatus => "usp_GetByID_FeedbackStatus";
}
