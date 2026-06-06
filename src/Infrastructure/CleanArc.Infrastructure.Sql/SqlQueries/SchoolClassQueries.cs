namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class SchoolClassQueries
    {
        public static string Create_SchoolClass => "usp_Create_SchoolClass";
        public static string Update_SchoolClass => "usp_Update_SchoolClass";
        public static string Delete_SchoolClass => "usp_Delete_SchoolClass";
        public static string GetAll_SchoolClass => "usp_GetAll_SchoolClass";
        public static string GetById_SchoolClass => "usp_GetById_SchoolClass";
        public static string AssignTeacher_SchoolClass => "usp_AssignTeacher_SchoolClass";
    }
}
