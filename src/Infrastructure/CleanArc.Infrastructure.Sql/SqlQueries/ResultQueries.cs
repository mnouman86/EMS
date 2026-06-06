namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class ResultQueries
    {
        public static string Create_Session => "usp_Create_ResultSession";
        public static string GetAll_Sessions => "usp_GetAll_ResultSessions";

        public static string Configure_GradeBands => "usp_Configure_GradeBands";
        public static string GetAll_GradeBands => "usp_GetAll_GradeBands";

        public static string Enter_Marks => "usp_Enter_Marks";
        public static string Get_MarksEntryGrid => "usp_Get_MarksEntryGrid";

        public static string PreLock_Report => "usp_PreLock_Report";
        public static string Lock_ClassResults => "usp_Lock_ClassResults";
        public static string Unlock_ClassResults => "usp_Unlock_ClassResults";

        public static string Get_ClassSheet => "usp_Get_ClassSheet";

        public static string Parent_SearchResult => "usp_Parent_SearchResult";
        public static string Get_StudentResultCard => "usp_Get_StudentResultCard";
        public static string Get_StudentResultMarks => "usp_Get_StudentResultMarks";
    }
}
