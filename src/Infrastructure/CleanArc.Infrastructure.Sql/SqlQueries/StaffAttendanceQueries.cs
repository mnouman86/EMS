namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class StaffAttendanceQueries
    {
        public static string Get_MyToday     => "usp_Get_MyStaffAttendanceToday";
        public static string Record_CheckIn  => "usp_Record_StaffCheckIn";
        public static string Record_CheckOut => "usp_Record_StaffCheckOut";
        public static string Get_History     => "usp_Get_StaffAttendanceHistory";
        public static string Get_Overview    => "usp_Get_StaffAttendanceOverview";
    }
}
