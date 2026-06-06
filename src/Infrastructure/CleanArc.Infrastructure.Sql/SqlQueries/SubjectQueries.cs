namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class SubjectQueries
    {
        public static string Create_Subject => "usp_Create_Subject";
        public static string Update_Subject => "usp_Update_Subject";
        public static string Delete_Subject => "usp_Delete_Subject";
        public static string GetAll_Subject => "usp_GetAll_Subject";
        public static string GetById_Subject => "usp_GetById_Subject";
        public static string MapSubjects_ToClass => "usp_MapSubjects_ToClass";
        public static string GetSubjects_ByClass => "usp_GetSubjects_ByClass";
    }
}
