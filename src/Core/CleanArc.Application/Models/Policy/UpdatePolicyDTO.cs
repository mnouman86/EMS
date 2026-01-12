using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.Policy;

public class UpdatePolicyDTO
{
     public int Id { get; set; }
    public int? RatePlanTypeID { get; set; }
    public int? RefundPolicyTypeLookUpID { get; set; }
    public decimal? DeductionPercentage { get; set; }
    public int UpdatedBy { get; set; }
     public int CultureId { get; set; }
    // public DateTime UpdatedAt { get; set; }
}
