using System;
using System.Collections.Generic;

namespace CleanArc.Application.Models.Fee
{
    public class FeeReceiptModel
    {
        public string SchoolName { get; set; } = "The Saviour's Secondary School (TSSS)";
        public string SchoolAddress { get; set; }
        public string ReceiptNo { get; set; }
        public DateTime ReceiptDate { get; set; }
        public string StudentCode { get; set; }
        public string StudentFullName { get; set; }
        public string FatherOrParentName { get; set; }
        public string ClassName { get; set; }
        public string PaymentMode { get; set; }
        public string ReferenceNo { get; set; }
        public string CashierName { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal BalanceRemaining { get; set; }
        public bool IsDuplicate { get; set; }
        public DateTime? ChequeClearanceDate { get; set; }
        public List<FeeReceiptLine> Lines { get; set; } = new();
    }

    public class FeeReceiptLine
    {
        public string FeeType { get; set; }
        public string Period { get; set; }
        public decimal Amount { get; set; }
    }
}
