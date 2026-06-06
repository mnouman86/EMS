namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class FinanceQueries
    {
        public static string Get_IncomeSummary => "usp_FIN_Get_IncomeSummary";
        public static string Get_IncomeByMonth => "usp_FIN_Get_IncomeByMonth";
        public static string Get_IncomeByClass => "usp_FIN_Get_IncomeByClass";
        public static string Get_IncomeByFeeType => "usp_FIN_Get_IncomeByFeeType";

        public static string Get_ExpenseSummary => "usp_FIN_Get_ExpenseSummary";
        public static string Get_ExpenseByMonth => "usp_FIN_Get_ExpenseByMonth";
        public static string Get_ExpenseByCategory => "usp_FIN_Get_ExpenseByCategory";

        public static string Get_PnL => "usp_FIN_Get_PnL";

        public static string Get_MonthlySummary => "usp_FIN_Get_MonthlySummary";
        public static string Get_AnnualSummary => "usp_FIN_Get_AnnualSummary";
        public static string Get_CategoryWiseExpense => "usp_FIN_Get_CategoryWiseExpense";
        public static string Get_FeeCollectionVsTarget => "usp_FIN_Get_FeeCollectionVsTarget";

        public static string Set_OpeningCashBalance => "usp_FIN_Set_OpeningCashBalance";
        public static string Get_OpeningCashBalance => "usp_FIN_Get_OpeningCashBalance";
        public static string Get_CashFlowLedger => "usp_FIN_Get_CashFlowLedger";
    }
}
