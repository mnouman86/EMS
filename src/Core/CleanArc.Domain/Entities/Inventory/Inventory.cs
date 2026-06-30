using System;

namespace CleanArc.Domain.Entities.Inventory
{
    public class InventoryCategory
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class InventoryItem
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? UnitOfMeasure { get; set; }    // Pieces / Reams / Boxes / Litres / Kg / Sets
        public string? Description { get; set; }
        public decimal ReorderLevel { get; set; }
        public decimal CurrentStock { get; set; }     // denormalized; updated by movement SPs
        public bool IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class InventoryPurchase
    {
        public int Id { get; set; }
        public string? PurchaseCode { get; set; }     // PUR-YYYY-NNNN
        public DateTime PurchaseDate { get; set; }
        public string? VendorName { get; set; }
        public string? VendorInvoiceNo { get; set; }
        public string? PaymentMode { get; set; }
        public decimal GrandTotal { get; set; }
        public string? Notes { get; set; }
        public string? AttachmentPath { get; set; }
        public int? LinkedExpenseId { get; set; }     // populated when EXP module ships (INV-02)
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime? CancelledAt { get; set; }
        public string? CancelReason { get; set; }
    }

    public class InventoryPurchaseLine
    {
        public int Id { get; set; }
        public int PurchaseId { get; set; }
        public int ItemId { get; set; }
        public string? ItemName { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
    }

    public class InventoryIssue
    {
        public int Id { get; set; }
        public string? IssueCode { get; set; }        // ISS-YYYY-NNNN
        public DateTime IssueDate { get; set; }
        public string? IssuedToType { get; set; }     // Class / Teacher / Department / Student
        public int? IssuedToId { get; set; }          // polymorphic FK (no enforced FK)
        public string? IssuedToName { get; set; }
        public string? Purpose { get; set; }
        public int? IssuedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime? CancelledAt { get; set; }
        public string? CancelReason { get; set; }
    }

    public class InventoryIssueLine
    {
        public int Id { get; set; }
        public int IssueId { get; set; }
        public int ItemId { get; set; }
        public string? ItemName { get; set; }
        public decimal Quantity { get; set; }
        public decimal ReturnedQuantity { get; set; } // running tally
    }

    public class InventoryReturn
    {
        public int Id { get; set; }
        public int IssueId { get; set; }
        public DateTime ReturnDate { get; set; }
        public string? Notes { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public class InventoryReturnLine
    {
        public int Id { get; set; }
        public int ReturnId { get; set; }
        public int IssueLineId { get; set; }
        public int ItemId { get; set; }
        public decimal Quantity { get; set; }
        public string? Condition { get; set; }        // Good / Damaged / Lost
        public string? Notes { get; set; }
    }

    public class InventoryAdjustment
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string? AdjustmentType { get; set; }   // Increase / Decrease
        public decimal Quantity { get; set; }
        public string? Reason { get; set; }
        public DateTime AdjustedAt { get; set; }
        public int? AdjustedBy { get; set; }
        public int? ApprovedByUserId { get; set; }
    }

    public class InventoryAlertSnooze
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public DateTime SnoozedUntil { get; set; }
        public string? Reason { get; set; }
        public int? SnoozedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    /* ---------- Query projections ---------- */

    public class StockDashboardRow
    {
        public int ItemId { get; set; }
        public string? ItemName { get; set; }
        public string? Code { get; set; }
        public string? CategoryName { get; set; }
        public string? UnitOfMeasure { get; set; }
        public decimal OpeningStock { get; set; }
        public decimal Purchased { get; set; }
        public decimal Issued { get; set; }
        public decimal Adjusted { get; set; }
        public decimal Returned { get; set; }
        public decimal CurrentStock { get; set; }
        public decimal ReorderLevel { get; set; }
        public string? Status { get; set; }       // OK / Low / Out
    }

    public class LowStockAlertRow
    {
        public int ItemId { get; set; }
        public string? ItemName { get; set; }
        public string? Code { get; set; }
        public string? CategoryName { get; set; }
        public decimal CurrentStock { get; set; }
        public decimal ReorderLevel { get; set; }
        public DateTime? LastPurchasedAt { get; set; }
        public DateTime? SnoozedUntil { get; set; }
    }

    public class ItemLedgerRow
    {
        public DateTime When { get; set; }
        public string? MovementType { get; set; }     // Purchase / Issue / Return / Adjustment
        public string? Reference { get; set; }
        public decimal Quantity { get; set; }         // + in, - out
        public decimal RunningStock { get; set; }
        public string? Notes { get; set; }
        public int SourceMovementId { get; set; }     // FK back to the originating record (purchase/issue/return/adjustment)
    }

    /* INV-04: one row per issue line, with the returnable balance (for the return screen) */
    public class InventoryIssueDetailRow
    {
        public int IssueId { get; set; }
        public string? IssueCode { get; set; }
        public DateTime IssueDate { get; set; }
        public string? IssuedToType { get; set; }
        public string? IssuedToName { get; set; }
        public string? Purpose { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime? CancelledAt { get; set; }
        public string? CancelReason { get; set; }
        public int IssueLineId { get; set; }
        public int ItemId { get; set; }
        public string? ItemName { get; set; }
        public string? UnitOfMeasure { get; set; }
        public decimal Quantity { get; set; }
        public decimal ReturnedQuantity { get; set; }
        public decimal RemainingQuantity { get; set; }
    }

    /* INV-02 view: one row per purchase line (for the Purchase view dialog). */
    public class InventoryPurchaseDetailRow
    {
        public int PurchaseId { get; set; }
        public string? PurchaseCode { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string? VendorName { get; set; }
        public string? VendorInvoiceNo { get; set; }
        public string? PaymentMode { get; set; }
        public decimal GrandTotal { get; set; }
        public string? Notes { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime? CancelledAt { get; set; }
        public string? CancelReason { get; set; }
        public int PurchaseLineId { get; set; }
        public int ItemId { get; set; }
        public string? ItemName { get; set; }
        public string? ItemCode { get; set; }
        public string? UnitOfMeasure { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
    }

    /* ---------- Issue request workflow (teacher → accountant) ---------- */

    public class InventoryIssueRequestRow
    {
        public int Id { get; set; }
        public string? RequestCode { get; set; }
        public DateTime RequestDate { get; set; }
        public int RequestedByUserId { get; set; }
        public string? RequestedByName { get; set; }
        public string? Purpose { get; set; }
        public string? Status { get; set; }       // Pending / Approved / Rejected / Fulfilled
        public int? ApprovedByUserId { get; set; }
        public string? ApprovedByName { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public string? RejectedReason { get; set; }
        public int? FulfilledIssueId { get; set; }
        public string? FulfilledIssueCode { get; set; }
        public int LineCount { get; set; }
        public decimal TotalQuantity { get; set; }
    }

    public class InventoryIssueRequestLineRow
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public int ItemId { get; set; }
        public string? ItemName { get; set; }
        public string? Code { get; set; }
        public string? UnitOfMeasure { get; set; }
        public decimal Quantity { get; set; }
        public decimal CurrentStock { get; set; }      // for accountant to sanity-check before approving
    }

    /* "My Issued Items" — what's been issued to the calling user (Teacher / Employee) */
    public class MyIssuedInventoryRow
    {
        public int IssueId { get; set; }
        public string? IssueCode { get; set; }
        public DateTime IssueDate { get; set; }
        public string? Purpose { get; set; }
        public int IssueLineId { get; set; }
        public int ItemId { get; set; }
        public string? ItemName { get; set; }
        public string? Code { get; set; }
        public string? CategoryName { get; set; }
        public string? UnitOfMeasure { get; set; }
        public decimal Quantity { get; set; }
        public decimal ReturnedQuantity { get; set; }
        public decimal RemainingQuantity { get; set; }
    }
}
