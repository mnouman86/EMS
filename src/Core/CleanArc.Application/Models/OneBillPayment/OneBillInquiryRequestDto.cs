using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.OneBillPayment
{
    public class OneBillInquiryRequestDto
    {
        public string RelationshipId { get; set; } = string.Empty;
        public string TransmissionDate { get; set; } = string.Empty;
        public string TransmissionTime { get; set; } = string.Empty;
        public string Stan { get; set; } = string.Empty;
        public string Rrn { get; set; } = string.Empty;
        public string DateLocalTran { get; set; } = string.Empty;
        public string TimeLocalTran { get; set; } = string.Empty;
        public string AcqInstCode { get; set; } = string.Empty;
        public string PinData { get; set; } = string.Empty;
        public string UtilityCompanyId { get; set; } = string.Empty;
        public string UtilityConsumerNumber { get; set; } = string.Empty;
        public string Reserved { get; set; } = string.Empty;
    }
}
