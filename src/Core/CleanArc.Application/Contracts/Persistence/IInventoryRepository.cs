using CleanArc.Application.Common;
using CleanArc.Application.Models.Inventory;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.Inventory;

namespace CleanArc.Application.Contracts.Persistence
{
    public interface IInventoryRepository
    {
        /* Catalogue (INV-01) */
        Task<ResponseEntity> UpsertCategoryAsync(UpsertCategoryDTO dto);
        Task<ListResponseWrapper<InventoryCategory>> GetCategoriesAsync();

        Task<ResponseEntity> UpsertItemAsync(UpsertItemDTO dto);
        Task<ResponseEntity> DeleteItemAsync(DeleteRequest req, int? updatedBy);
        Task<ListResponseWrapper<InventoryItem>> GetItemsAsync(SearchRequest req);
        Task<SingleResponseWrapper<InventoryItem>> GetItemByIdAsync(int id);

        /* Movements (INV-02 / INV-03 / INV-04 / INV-07) */
        Task<ResponseEntity> RecordPurchaseAsync(RecordPurchaseDTO dto);
        Task<ResponseEntity> IssueItemsAsync(IssueItemsDTO dto);
        Task<ResponseEntity> RecordReturnAsync(RecordReturnDTO dto);
        Task<ResponseEntity> RecordAdjustmentAsync(StockAdjustmentDTO dto);

        /* Dashboard / Alerts / Reports (INV-05 / INV-06 / INV-08) */
        Task<ListResponseWrapper<StockDashboardRow>> GetStockDashboardAsync(int? categoryId, string status, System.DateTime fromDate, System.DateTime toDate);
        Task<ListResponseWrapper<LowStockAlertRow>> GetLowStockAlertsAsync();
        Task<ResponseEntity> SnoozeAlertAsync(SnoozeAlertDTO dto);

        Task<ListResponseWrapper<InventoryPurchase>> GetPurchaseRegisterAsync(
            System.DateTime fromDate, System.DateTime toDate,
            int? categoryId, string? vendorName);
        Task<ListResponseWrapper<InventoryIssue>> GetIssueRegisterAsync(
            System.DateTime fromDate, System.DateTime toDate,
            int? categoryId, int? itemId, string? issuedToType, int? issuedToId, int? issuedByUserId);
        Task<ListResponseWrapper<ItemLedgerRow>> GetItemLedgerAsync(int itemId, System.DateTime fromDate, System.DateTime toDate);
        Task<ListResponseWrapper<InventoryIssueDetailRow>> GetIssueDetailAsync(int issueId);

        /* Inventory Issue Request workflow (new — teacher requests, accountant approves/fulfills) */
        Task<ResponseEntity> CreateIssueRequestAsync(CreateInventoryIssueRequestDTO dto);
        Task<ResponseEntity> ApproveIssueRequestAsync(int requestId, int approvedByUserId);
        Task<ResponseEntity> RejectIssueRequestAsync(int requestId, int rejectedByUserId, string reason);
        Task<ResponseEntity> FulfillIssueRequestAsync(int requestId, int fulfilledByUserId, System.DateTime issueDate);
        Task<ListResponseWrapper<InventoryIssueRequestRow>> GetIssueRequestsAsync(
            int callerUserId, bool mineOnly, string? status, System.DateTime? fromDate, System.DateTime? toDate);
        Task<ListResponseWrapper<InventoryIssueRequestLineRow>> GetIssueRequestLinesAsync(int requestId);

        /* "My Issued Items" — staff self-service view */
        Task<ListResponseWrapper<MyIssuedInventoryRow>> GetMyIssuedAsync(int callerUserId, System.DateTime fromDate, System.DateTime toDate);
    }
}
