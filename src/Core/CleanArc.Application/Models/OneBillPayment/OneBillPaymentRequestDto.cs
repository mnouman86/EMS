using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.OneBillPayment
{
    public class OneBillPaymentRequestDto
    {
        public string RelationshipId { get; set; }
        public string TransmissionDate { get; set; }
        public string TransmissionTime { get; set; }
        public string Stan { get; set; }
        public string Rrn { get; set; }
        public string DateLocalTran { get; set; }
        public string TimeLocalTran { get; set; }
        public string AcqInstCode { get; set; }
        public string PinData { get; set; }
        public string UtilityCompanyId { get; set; }
        public string UtilityConsumerNumber { get; set; }
        public string TransactionAmount { get; set; }
        public string TransactionCurrency { get; set; }
        public string Reserved { get; set; }
    }
}
