using System;

namespace CleanArc.Application.Models.Fee
{
    /* ---------- Fee Type / Amount / Calendar ---------- */

    public class UpsertFeeTypeDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Category { get; set; }
        public bool IsActive { get; set; }
        public int UpdatedBy { get; set; }
    }

    public class UpsertFeeTypeAmountDTO
    {
        public int FeeTypeId { get; set; }
        public int SchoolClassId { get; set; }
        public decimal Amount { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public int CreatedBy { get; set; }
    }

    public class ConfigureFeeCalendarDTO
    {
        public int FeeTypeId { get; set; }
        public int AcademicYearId { get; set; }
        public string BilledMonthsCsv { get; set; }  // e.g. "9,10,11,12,1,2,3,4,5,6"
        public int UpdatedBy { get; set; }
    }

    /* ---------- Invoices ---------- */

    public class GenerateMonthlyInvoicesDTO
    {
        public int AcademicYearId { get; set; }
        public int BillingMonth { get; set; }
        public int BillingYear { get; set; }
        public int? ClassId { get; set; }              // null = all classes
        public string ExcludedStudentIdsCsv { get; set; } // optional, from preview deselect
        public bool DryRun { get; set; }
        public int CreatedBy { get; set; }
    }

    public class CancelInvoiceDTO
    {
        public int InvoiceId { get; set; }
        public string Reason { get; set; }
        public int UpdatedBy { get; set; }
    }

    /* ---------- Payments ---------- */

    public class RecordPaymentDTO
    {
        public int StudentId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMode { get; set; }        // Cash / Cheque / BankTransfer / Online
        public string ReferenceNo { get; set; }
        public string AllocationsJson { get; set; }    // [ { InvoiceId, Amount } ]
        public string Remarks { get; set; }
        public int CollectingStaffId { get; set; }
        public int CreatedBy { get; set; }
    }

    public class ClearChequeDTO
    {
        public int PaymentId { get; set; }
        public DateTime ClearanceDate { get; set; }
        public bool Bounced { get; set; }
        public int UpdatedBy { get; set; }
    }

    public class ApplyAdvanceDTO
    {
        public int StudentId { get; set; }
        public int InvoiceId { get; set; }
        public decimal Amount { get; set; }
        public int UpdatedBy { get; set; }
    }

    public class ReversePaymentDTO
    {
        public int PaymentId { get; set; }
        public string Reason { get; set; }
        public int ActorUserId { get; set; }
    }

    /* ---------- Concession ---------- */

    public class GrantConcessionDTO
    {
        public int StudentId { get; set; }
        public string ConcessionType { get; set; }     // Percentage / Fixed / Sibling / FullWaiver
        public decimal Value { get; set; }
        public string ApplicableFeeTypeIdsCsv { get; set; }  // empty = all
        public DateTime EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
        public string Reason { get; set; }
        public int? ApprovedByUserId { get; set; }
        public int CreatedBy { get; set; }
    }

    public class RevokeConcessionDTO
    {
        public int ConcessionId { get; set; }
        public string Reason { get; set; }
        public int UpdatedBy { get; set; }
    }

    /* ---------- Reminders ---------- */

    public class SendFeeRemindersDTO
    {
        public string StudentIdsCsv { get; set; }      // empty = all with outstanding
        public string Channel { get; set; }            // Whatsapp / Sms / Both
        public string TemplateKey { get; set; }        // optional
        public int SentBy { get; set; }
    }

    /* ---------- Arrears ---------- */

    public class CarryForwardArrearsDTO
    {
        public int FromAcademicYearId { get; set; }
        public int ToAcademicYearId { get; set; }
        public int UpdatedBy { get; set; }
    }

    public class WriteOffArrearDTO
    {
        public int ArrearId { get; set; }
        public string Reason { get; set; }
        public int ApprovedByUserId { get; set; }
    }

    /* ---------- Parent search (FEE-13) ---------- */

    public class ParentFeeSearchDTO
    {
        public string StudentCode { get; set; }
        public string SecondFactor { get; set; }       // DOB or PIN
        public string ClientIp { get; set; }
    }
}
