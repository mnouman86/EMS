namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class StudentQueries
    {
        public static string Create_Student => "usp_SubmitAdmission_Student";
        public static string Update_Student => "usp_Update_Student";
        public static string Delete_Student => "usp_Delete_Student";
        public static string GetAll_Student => "usp_GetAll_Student";
        public static string GetById_Student => "usp_GetById_Student";
        public static string ChangeStatus_Student => "usp_ChangeStatus_Student";
        public static string BulkImport_Students => "usp_BulkImport_Students";
        public static string Promote_Students => "usp_Promote_Students";
    }
}
