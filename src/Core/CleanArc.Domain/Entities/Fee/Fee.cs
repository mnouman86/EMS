using System;

namespace CleanArc.Domain.Entities.Fee
{
    /* ============================ Configuration ============================ */

    public class FeeType
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }               // unique
        public string? Category { get; set; }           // OneTime / Monthly / Annual / Periodic
        public bool IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class FeeTypeAmount
    {
        public int Id { get; set; }
        public int FeeTypeId { get; set; }
        public int SchoolClassId { get; set; }
        public decimal Amount { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public class FeeTypeCalendar
    {
        public int Id { get; set; }
        public int FeeTypeId { get; set; }
        public int AcademicYearId { get; set; }
        public int MonthNumber { get; set; }            // 1..12
        public bool IsBilled { get; set; }
    }

    /* ============================ Invoices ============================ */

    public class FeeInvoice
    {
        public int Id { get; set; }
        public string? InvoiceNo { get; set; }          // INV-YYYY-NNNNNN
        public int StudentId { get; set; }
        public int AcademicYearId { get; set; }
        public int? BillingMonth { get; set; }          // 1..12 for Monthly; null for one-time / annual
        public int? BillingYear { get; set; }
        public int ClassIdSnapshot { get; set; }
        public string? ClassNameSnapshot { get; set; }
        public decimal TotalDue { get; set; }
        public decimal ConcessionApplied { get; set; }
        public decimal NetDue { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal BalanceDue { get; set; }
        public string? Status { get; set; }             // Unpaid / Partial / Paid / Overdue / Cancelled
        public DateTime DueDate { get; set; }
        public DateTime GeneratedAt { get; set; }
        public bool IsCancelled { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class FeeInvoiceLine
    {
        public int Id { get; set; }
        public int FeeInvoiceId { get; set; }
        public int FeeTypeId { get; set; }
        public string? FeeTypeNameSnapshot { get; set; }
        public decimal BaseAmount { get; set; }
        public decimal ConcessionAmount { get; set; }
        public decimal NetAmount { get; set; }
    }

    /* ============================ Payments ============================ */

    public class FeePayment
    {
        public int Id { get; set; }
        public string? ReceiptNo { get; set; }          // TSSS-YYYY-NNNNNN
        public int StudentId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal AppliedToInvoices { get; set; }  // sum of allocations
        public decimal AppliedToAdvance { get; set; }   // surplus -> advance
        public string? PaymentMode { get; set; }        // Cash / Cheque / BankTransfer / Online
        public string? ReferenceNo { get; set; }
        public string? ChequeStatus { get; set; }       // PendingClearance / Cleared / Bounced (null for non-cheque)
        public DateTime? ClearanceDate { get; set; }
        public int? CollectingStaffId { get; set; }
        public string? Remarks { get; set; }
        public bool IsReversed { get; set; }
        public int? ReversalRefPaymentId { get; set; }  // points to the counter-entry
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public class FeePaymentAllocation
    {
        public int Id { get; set; }
        public int FeePaymentId { get; set; }
        public int FeeInvoiceId { get; set; }
        public decimal AmountAllocated { get; set; }
    }

    /* ============================ Advance / Concession / Arrears ============================ */

    public class StudentAdvanceBalance
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public decimal AvailableBalance { get; set; }
        public DateTime LastUpdatedAt { get; set; }
    }

    public class FeeConcession
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string? ConcessionType { get; set; }     // Percentage / Fixed / Sibling / FullWaiver
        public decimal Value { get; set; }              // % or amount
        public string? ApplicableFeeTypesJson { get; set; } // e.g. "[1,2,5]" or "[]" = all
        public DateTime EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
        public string? Reason { get; set; }
        public int? ApprovedByUserId { get; set; }
        public string? Status { get; set; }             // Active / Expired / Revoked
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class FeeArrear
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int FromAcademicYearId { get; set; }
        public int ToAcademicYearId { get; set; }
        public decimal Amount { get; set; }
        public bool IsWrittenOff { get; set; }
        public string? WriteOffReason { get; set; }
        public int? WrittenOffByUserId { get; set; }
        public DateTime? WrittenOffAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class FeePaymentReversal
    {
        public int Id { get; set; }
        public int OriginalPaymentId { get; set; }
        public int CounterPaymentId { get; set; }       // the reversing payment row
        public string? Reason { get; set; }
        public int ActorUserId { get; set; }
        public DateTime At { get; set; }
    }

    /* ============================ Query projections ============================ */

    /* FEE-12: arrears list projection (joined with student for display) */
    public class FeeArrearRow
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string? StudentCode { get; set; }
        public string? StudentFullName { get; set; }
        public int FromAcademicYearId { get; set; }
        public int ToAcademicYearId { get; set; }
        public string? FromYearName { get; set; }
        public string? ToYearName { get; set; }
        public decimal Amount { get; set; }
        public bool IsWrittenOff { get; set; }
        public string? WriteOffReason { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class FeeStructureRow
    {
        public int FeeTypeId { get; set; }
        public string? FeeTypeName { get; set; }
        public string? FeeTypeCode { get; set; }
        public string? Category { get; set; }
        public int SchoolClassId { get; set; }
        public string? ClassName { get; set; }
        public decimal Amount { get; set; }
        public DateTime EffectiveFrom { get; set; }
    }

    public class InvoicePreviewRow
    {
        public int StudentId { get; set; }
        public string? StudentCode { get; set; }
        public string? StudentFullName { get; set; }
        public int ClassId { get; set; }
        public string? ClassName { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal ConcessionAmount { get; set; }
        public decimal NetAmount { get; set; }
        public string? Note { get; set; }               // e.g. "Already invoiced"
    }

    public class StudentLedgerEntry
    {
        public DateTime Date { get; set; }
        public string? Type { get; set; }               // Invoice / Payment / Reversal / Concession / Arrear
        public string? Reference { get; set; }          // InvoiceNo or ReceiptNo
        public string? Description { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal RunningBalance { get; set; }
    }

    /* Class Fee Board — one row per active student in a class, used by the
       bulk-collection screen. Period filter (year/month) is optional. */
    public class FeeClassBoardRow
    {
        public int StudentId { get; set; }
        public string? StudentCode { get; set; }
        public string? FormNo { get; set; }
        public string? FullName { get; set; }
        public string? ParentName { get; set; }
        public string? ParentMobile { get; set; }
        public decimal TotalInvoiced { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal Outstanding { get; set; }
        public DateTime? LastPaymentDate { get; set; }
        public decimal? LastPaymentAmount { get; set; }
        public string? MonthsDue { get; set; }
        public int InvoiceCount { get; set; }
    }

    public class PendingFeeRow
    {
        public int StudentId { get; set; }
        public string? StudentCode { get; set; }
        public string? StudentFullName { get; set; }
        public int ClassId { get; set; }
        public string? ClassName { get; set; }
        public string? MonthsDue { get; set; }
        public decimal TotalOutstanding { get; set; }
        public int DaysOverdue { get; set; }
        public string? ParentMobile { get; set; }
        public string? EmergencyContactPhone { get; set; }
        public bool HasActiveWaiver { get; set; }
    }

    public class CollectionDashboardSummary
    {
        public decimal TotalCollected { get; set; }
        public decimal TotalPending { get; set; }
        public decimal TotalInvoiced { get; set; }
        public decimal CollectionRatePercent { get; set; }
    }

    public class CollectionByClassRow
    {
        public int ClassId { get; set; }
        public string? ClassName { get; set; }
        public int StudentCount { get; set; }
        public decimal Invoiced { get; set; }
        public decimal Collected { get; set; }
        public decimal Pending { get; set; }
        public decimal RatePercent { get; set; }
    }

    public class CollectionByDayRow
    {
        public DateTime Day { get; set; }
        public decimal Amount { get; set; }
    }

    public class FeeReportAgeingRow
    {
        public int StudentId { get; set; }
        public string? StudentCode { get; set; }
        public string? StudentFullName { get; set; }
        public decimal Bucket0_30 { get; set; }
        public decimal Bucket31_60 { get; set; }
        public decimal Bucket61_90 { get; set; }
        public decimal Bucket91Plus { get; set; }
        public decimal Total { get; set; }
    }

    public class ParentFeeSummary
    {
        public int StudentId { get; set; }
        public string? StudentCode { get; set; }
        public string? StudentFullName { get; set; }
        public string? CurrentMonthStatus { get; set; }
        public decimal TotalOutstanding { get; set; }
        public DateTime? LastPaymentDate { get; set; }
    }
}
