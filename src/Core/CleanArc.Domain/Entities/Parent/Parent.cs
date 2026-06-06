using System;

namespace CleanArc.Domain.Entities.Parent
{
    public class ParentChildRow
    {
        public int StudentId { get; set; }
        public string? StudentCode { get; set; }
        public string? FormNo { get; set; }
        public string? FullName { get; set; }
        public int? ClassId { get; set; }
        public string? ClassName { get; set; }
        public string? Status { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Relationship { get; set; }
    }

    public class ParentLinkCheckRow
    {
        public bool IsLinked { get; set; }
    }

    public class StudentPaymentRow
    {
        public int PaymentId { get; set; }
        public string? ReceiptNo { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string? PaymentMode { get; set; }
        public string? ReferenceNo { get; set; }
    }
}
