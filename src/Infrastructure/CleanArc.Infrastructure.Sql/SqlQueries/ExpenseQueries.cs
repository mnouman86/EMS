namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class ExpenseQueries
    {
        public static string Upsert_Category => "usp_Upsert_ExpenseCategory";
        public static string Delete_Category => "usp_Delete_ExpenseCategory";
        public static string Get_Categories => "usp_Get_ExpenseCategories";

        public static string Upsert_RecurringTemplate => "usp_Upsert_RecurringExpenseTemplate";
        public static string Get_RecurringTemplates => "usp_Get_RecurringExpenseTemplates";
        public static string Generate_RecurringExpenses => "usp_Generate_RecurringExpenses";

        public static string Record_Expense => "usp_Record_Expense";
        public static string Delete_Expense => "usp_Delete_Expense";
        public static string Get_Expenses => "usp_Get_Expenses";

        public static string Get_BudgetMonitoring => "usp_Get_BudgetMonitoring";

        public static string Start_PayrollRun => "usp_Start_PayrollRun";
        public static string Adjust_PayrollEntry => "usp_Adjust_PayrollEntry";
        public static string Confirm_PayrollRun => "usp_Confirm_PayrollRun";
        public static string Get_PayrollRuns => "usp_Get_PayrollRuns";
        public static string Get_PayrollEntries => "usp_Get_PayrollEntries";
        public static string Get_PayrollEntry => "usp_Get_PayrollEntry";
    }
}
