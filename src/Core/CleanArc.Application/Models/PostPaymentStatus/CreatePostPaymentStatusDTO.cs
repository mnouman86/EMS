using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.PostPaymentStatus
{
    public class CreatePostPaymentStatusDTO
    {
        public string? Bill_Status { get; set; }
        public string? Initiator { get; set; }
        public string? Instrument_Institution { get; set; }
        public string? Instrument_Number { get; set; }
        public string? Instrument_Type { get; set; }
        public string? Message { get; set; }
        public decimal Paid_Amount { get; set; }
        public string? Payment_Channel { get; set; }
        public string? Payment_Link { get; set; }
        public string? Reference_Number { get; set; }
        public string? Order_Number { get; set; }
        public int Status { get; set; }
        public string? Transaction_Date_Time { get; set; }
        public string? Transaction_Ref_Id { get; set; }
        public int? CreatedBy { get; set; } // int NULL
        public int? CultureId { get; set; }

    }

   
}
