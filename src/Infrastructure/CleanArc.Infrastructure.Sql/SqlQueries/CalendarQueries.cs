namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class CalendarQueries
    {
        public static string Get_SchoolHolidays   => "usp_Get_SchoolHolidays";
        public static string Upsert_SchoolHoliday => "usp_Upsert_SchoolHoliday";
        public static string Delete_SchoolHoliday => "usp_Delete_SchoolHoliday";
        public static string Get_CalendarConfig   => "usp_Get_CalendarConfig";
        public static string Set_CalendarConfig   => "usp_Set_CalendarConfig";
        public static string Get_NonWorkingDates  => "usp_Get_NonWorkingDates";
    }
}
