namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class ParentQueries
    {
        public static string Link_StudentParent    => "usp_Link_StudentParent";
        public static string Unlink_StudentParent   => "usp_Unlink_StudentParent";
        public static string Get_ParentChildren     => "usp_Get_ParentChildren";
        public static string Is_ParentOfStudent     => "usp_Is_ParentOfStudent";
        public static string Get_StudentPayments    => "usp_Get_StudentPayments";
    }
}
