using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.OneBillPayment
{
    public class OneBillPaymentResponseDto
    {
        public string ResponseCode { get; set; }
        public string AuthIdResponse { get; set; }
        public string TransactionLogId { get; set; }
        public string Reserved { get; set; }
    }
}
