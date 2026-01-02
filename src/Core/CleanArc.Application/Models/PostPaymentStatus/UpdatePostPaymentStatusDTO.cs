using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.PostPaymentStatus
{
    public class UpdatePostPaymentStatusDTO
    {
        public int Id { get; set; } // IDENTITY(1,1) NOT NULL
        public string? BillStatus { get; set; }
        public string? Initiator { get; set; }
        public string? InstrumentInstitution { get; set; }
        public string? InstrumentNumber { get; set; }
        public string? InstrumentType { get; set; }
        public string? Message { get; set; }
        public decimal PaidAmount { get; set; }
        public string? PaymentChannel { get; set; }
        public string? PaymentLink { get; set; }
        public string? ReferenceNumber { get; set; }
        public string? OrderNumber { get; set; }
        public int Status { get; set; }
        public string? TransactionDateTime { get; set; }
        public string? TransactionRefId { get; set; }
        public int? UpdatedBy { get; set; } // int NULL
        public int? CultureId { get; set; }
        public string PSID { get; set; }
        public string ApplicableSoc { get; set; }
        public string BillerActualSettlementDateTime { get; set; }
        public string BillerSettlementAmount { get; set; }
        public string BillerSettlementDate { get; set; }
        public string BillerSettlementRefId { get; set; }
        public string BillerSettlementStatus { get; set; }
        public string BusinessCrn { get; set; }
        public decimal Fee { get; set; }
        public string FeeChargingType { get; set; }
        public string Qr { get; set; }
        public string SettlementInstitution { get; set; }
        public decimal TaxOnFee { get; set; }
    }
}
