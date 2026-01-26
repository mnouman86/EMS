using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.RefundPolicy;

public class UpdateRefundPolicyDTO
{
     public int Id { get; set; }
    public int? GenericTitleId { get; set; }
    public int? ServiceTypeEnumId { get; set; }
    public string Description { get; set; }

    public int? RefundPolicyTypeLookUpID { get; set; }
    public decimal? DeductionPercentage { get; set; }
    public int UpdatedBy { get; set; }
     public int CultureId { get; set; }
    // public DateTime UpdatedAt { get; set; }
}
