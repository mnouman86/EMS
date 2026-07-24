using System;
using System.Collections.Generic;

namespace CleanArc.Application.Models.Fee
{
    /// <summary>Model handed to FeeInvoicePdfRenderer.</summary>
    public class FeeInvoiceModel
    {
        public string SchoolName { get; set; } = "TSSS · The STEM Sprout School";
        public string? SchoolAddress { get; set; }

        public string InvoiceNo { get; set; } = string.Empty;
        public string AcademicYear { get; set; } = string.Empty;
        public int BillingMonth { get; set; }
        public int BillingYear { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime GeneratedAt { get; set; }
        public string Status { get; set; } = "Unpaid";

        // Student header
        public string StudentCode { get; set; } = string.Empty;
        public string StudentFullName { get; set; } = string.Empty;
        public string ClassName { get; set; } = string.Empty;
        public string? ParentName { get; set; }
        public string? ParentMobile { get; set; }
        public string? EmergencyPhone { get; set; }
        public string? HomeAddress { get; set; }

        // Amounts
        public decimal TotalDue { get; set; }
        public decimal ConcessionApplied { get; set; }
        public decimal NetDue { get; set; }
        public decimal PriorArrearsBrought { get; set; }
        public decimal TotalPayable { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal BalanceDue { get; set; }

        // Lines
        public List<FeeInvoiceLineModel> Lines { get; set; } = new();

        // Override history
        public List<FeeInvoiceOverrideModel> Overrides { get; set; } = new();

        // Cancellation info (renders CANCELLED watermark if true)
        public bool IsCancelled { get; set; }
        public string? CancelReason { get; set; }
    }

    public class FeeInvoiceLineModel
    {
        public string FeeTypeName { get; set; } = string.Empty;
        public decimal BaseAmount { get; set; }
        public decimal ConcessionAmount { get; set; }
        public decimal NetAmount { get; set; }
    }

    public class FeeInvoiceOverrideModel
    {
        public decimal OriginalNetAmount { get; set; }
        public decimal OverrideNetAmount { get; set; }
        public string Reason { get; set; } = string.Empty;
        public DateTime ActorAt { get; set; }
    }
}
