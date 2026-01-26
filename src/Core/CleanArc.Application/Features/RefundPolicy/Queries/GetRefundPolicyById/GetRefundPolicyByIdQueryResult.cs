using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.RefundPolicy.Queries.GetRefundPolicyById
{
    public class GetRefundPolicyByIdQueryResult
    {

        public int Id { get; set; }
        public int? GenericTitleId { get; set; }
        public int? ServiceTypeEnumId { get; set; }
        public string Description { get; set; }

        public int? RefundPolicyTypeLookUpID { get; set; }
        public string? RefundPolicyType { get; set; }
        public decimal? DeductionPercentage { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
