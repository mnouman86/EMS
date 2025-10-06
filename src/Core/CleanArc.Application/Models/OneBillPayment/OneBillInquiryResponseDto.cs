using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.OneBillPayment
{
    public class OneBillInquiryResponseDto
    {
        public string ResponseCode { get; set; } = "00";
        public string ConsumerDetail { get; set; } = string.Empty;
        public string LoanStatus { get; set; } = string.Empty;
        public string DueDate { get; set; } = string.Empty;
        public string AmountDueDate { get; set; } = string.Empty;
        public string AmountAfterDueDate { get; set; } = string.Empty;
        public string InstallmentNo { get; set; } = string.Empty;
        public string RemainingInstallments { get; set; } = string.Empty;
        public string RemainingInstallmentAmount { get; set; } = string.Empty;
        public string NextPaymentDueDate { get; set; } = string.Empty;
        public string Reserved { get; set; } = string.Empty;
    }
}
