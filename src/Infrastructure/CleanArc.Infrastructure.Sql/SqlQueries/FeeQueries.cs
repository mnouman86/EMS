namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class FeeQueries
    {
        // Configuration
        public static string Upsert_FeeType => "usp_Upsert_FeeType";
        public static string Delete_FeeType => "usp_Delete_FeeType";
        public static string GetAll_FeeTypes => "usp_GetAll_FeeTypes";
        public static string Upsert_FeeTypeAmount => "usp_Upsert_FeeTypeAmount";
        public static string Get_FeeStructureForClass => "usp_Get_FeeStructureForClass";
        public static string Configure_FeeCalendar => "usp_Configure_FeeCalendar";

        // Invoices
        public static string Generate_MonthlyInvoices => "usp_Generate_MonthlyInvoices";
        public static string Cancel_FeeInvoice => "usp_Cancel_FeeInvoice";

        // Payments
        public static string Record_FeePayment => "usp_Record_FeePayment";
        public static string Clear_Cheque => "usp_Clear_Cheque";
        public static string Apply_Advance => "usp_Apply_Advance";
        public static string Reverse_FeePayment => "usp_Reverse_FeePayment";

        // Ledger / receipts
        public static string Get_StudentLedger => "usp_Get_StudentLedger";
        public static string Get_PaymentById => "usp_Get_FeePaymentById";
        public static string Get_PaymentAllocations => "usp_Get_FeePaymentAllocations";

        // Concession
        public static string Grant_Concession => "usp_Grant_FeeConcession";
        public static string Revoke_Concession => "usp_Revoke_FeeConcession";
        public static string Get_Concessions => "usp_Get_FeeConcessions";

        // Reminders
        public static string Get_ReminderTargets => "usp_Get_FeeReminderTargets";

        // Arrears
        public static string CarryForward_Arrears => "usp_CarryForward_Arrears";
        public static string WriteOff_Arrear => "usp_WriteOff_Arrear";
        public static string Get_Arrears => "usp_Get_FeeArrears";

        // Dashboards / Reports
        public static string Get_CollectionSummary => "usp_Get_CollectionSummary";
        public static string Get_CollectionByClass => "usp_Get_CollectionByClass";
        public static string Get_CollectionByDay => "usp_Get_CollectionByDay";
        public static string Get_PendingFeeList => "usp_Get_PendingFeeList";
        public static string Get_MonthlyNonSubmitted => "usp_Get_MonthlyNonSubmitted";
        public static string Get_OutstandingAgeing => "usp_Get_OutstandingAgeing";

        // Advance / Parent
        public static string Get_AdvanceBalance => "usp_Get_StudentAdvanceBalance";
        public static string Parent_FeeSearch => "usp_Parent_FeeSearch";

        // Class Fee Board — per-student state for a class (bulk-collection screen)
        public static string Get_FeeClassBoard => "usp_Get_FeeClassBoard";
    }
}
