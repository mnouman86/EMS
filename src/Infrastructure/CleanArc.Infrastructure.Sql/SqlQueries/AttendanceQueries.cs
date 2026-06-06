namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class AttendanceQueries
    {
        public static string Get_ClassAttendanceGrid       => "usp_Get_ClassAttendanceGrid";
        public static string BulkSave_ClassAttendance      => "usp_BulkSave_ClassAttendance";
        public static string Get_StudentAttendanceHistory  => "usp_Get_StudentAttendanceHistory";
        public static string Get_StudentAttendanceSummary  => "usp_Get_StudentAttendanceSummary";
    }
}
