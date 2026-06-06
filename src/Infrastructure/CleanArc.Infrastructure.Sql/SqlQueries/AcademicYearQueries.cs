namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class AcademicYearQueries
    {
        public static string Create_AcademicYear => "usp_Create_AcademicYear";
        public static string Update_AcademicYear => "usp_Update_AcademicYear";
        public static string SetCurrent_AcademicYear => "usp_SetCurrent_AcademicYear";
        public static string Delete_AcademicYear => "usp_Delete_AcademicYear";
        public static string GetAll_AcademicYear => "usp_GetAll_AcademicYear";
        public static string GetById_AcademicYear => "usp_GetById_AcademicYear";
        public static string GetCurrent_AcademicYear => "usp_GetCurrent_AcademicYear";
    }
}
