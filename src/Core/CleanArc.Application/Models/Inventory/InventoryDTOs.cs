using System;

namespace CleanArc.Application.Models.Inventory
{
    /* ---------- Catalogue ---------- */

    public class UpsertCategoryDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int UpdatedBy { get; set; }
    }

    public class UpsertItemDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int CategoryId { get; set; }
        public string UnitOfMeasure { get; set; }
        public string Description { get; set; }
        public decimal ReorderLevel { get; set; }
        public bool IsActive { get; set; }
        public int UpdatedBy { get; set; }
    }

    /* ---------- Movements ---------- */

    public class RecordPurchaseDTO
    {
        public DateTime PurchaseDate { get; set; }
        public string VendorName { get; set; }
        public string VendorInvoiceNo { get; set; }
        public string PaymentMode { get; set; }
        public string Notes { get; set; }
        public string AttachmentPath { get; set; }
        public string LinesJson { get; set; }      // [{ItemId, Quantity, UnitPrice}]
        public int CreatedBy { get; set; }
    }

    public class IssueItemsDTO
    {
        public DateTime IssueDate { get; set; }
        public string IssuedToType { get; set; }   // Class / Teacher / Department / Student
        public int? IssuedToId { get; set; }
        public string IssuedToName { get; set; }
        public string Purpose { get; set; }
        public string LinesJson { get; set; }      // [{ItemId, Quantity}]
        public int IssuedBy { get; set; }
    }

    public class RecordReturnDTO
    {
        public int IssueId { get; set; }
        public DateTime ReturnDate { get; set; }
        public string Notes { get; set; }
        public string LinesJson { get; set; }      // [{IssueLineId, ItemId, Quantity, Condition, Notes}]
        public int CreatedBy { get; set; }
    }

    public class StockAdjustmentDTO
    {
        public int ItemId { get; set; }
        public string AdjustmentType { get; set; } // Increase / Decrease
        public decimal Quantity { get; set; }
        public string Reason { get; set; }
        public int AdjustedBy { get; set; }
        public int? ApprovedByUserId { get; set; }
    }

    /* ---------- Alerts ---------- */

    public class SnoozeAlertDTO
    {
        public int ItemId { get; set; }
        public int SnoozeForDays { get; set; }
        public string Reason { get; set; }
        public int SnoozedBy { get; set; }
    }
}
