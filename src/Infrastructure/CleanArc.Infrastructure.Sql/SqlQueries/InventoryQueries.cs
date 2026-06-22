namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class InventoryQueries
    {
        public static string Upsert_Category => "usp_Upsert_InventoryCategory";
        public static string GetAll_Categories => "usp_GetAll_InventoryCategories";

        public static string Upsert_Item => "usp_Upsert_InventoryItem";
        public static string Delete_Item => "usp_Delete_InventoryItem";
        public static string GetAll_Items => "usp_GetAll_InventoryItems";
        public static string Get_ItemById => "usp_Get_InventoryItemById";

        public static string Record_Purchase => "usp_Record_InventoryPurchase";
        public static string Issue_Items => "usp_Issue_InventoryItems";
        public static string Record_Return => "usp_Record_InventoryReturn";
        public static string Record_Adjustment => "usp_Record_InventoryAdjustment";

        public static string Get_StockDashboard => "usp_Get_InventoryStockDashboard";
        public static string Get_LowStockAlerts => "usp_Get_InventoryLowStockAlerts";
        public static string Snooze_Alert => "usp_Snooze_InventoryAlert";

        public static string Get_PurchaseRegister => "usp_Get_InventoryPurchaseRegister";
        public static string Get_IssueRegister => "usp_Get_InventoryIssueRegister";
        public static string Get_ItemLedger => "usp_Get_InventoryItemLedger";
        public static string Get_IssueDetail => "usp_Get_InventoryIssueDetail";

        /* Issue request workflow (new) */
        public static string Create_IssueRequest  => "usp_Create_InventoryIssueRequest";
        public static string Approve_IssueRequest => "usp_Approve_InventoryIssueRequest";
        public static string Reject_IssueRequest  => "usp_Reject_InventoryIssueRequest";
        public static string Fulfill_IssueRequest => "usp_Fulfill_InventoryIssueRequest";
        public static string Get_IssueRequests    => "usp_Get_InventoryIssueRequests";
        public static string Get_IssueRequestLines=> "usp_Get_InventoryIssueRequestLines";

        /* My Issued (per-user) */
        public static string Get_MyIssued => "usp_Get_InventoryMyIssued";
    }
}
