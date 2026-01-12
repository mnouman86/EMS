using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.Policy.Queries.GetPolicyById
{
    public class GetPolicyByIdQueryResult
    {

        public int Id { get; set; }
        public int? RatePlanTypeID { get; set; }
        public int? RefundPolicyTypeLookUpID { get; set; }
        public decimal? DeductionPercentage { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
