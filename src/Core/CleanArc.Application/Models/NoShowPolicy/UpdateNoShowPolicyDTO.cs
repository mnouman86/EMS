using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.NoShowPolicy;

public class UpdateNoShowPolicyDTO
{
     public int Id { get; set; }
    public int? RatePlanTypeID { get; set; }
    public decimal? DeductionPercentage { get; set; }
    public bool? OneNight { get; set; }
    public int UpdatedBy { get; set; }
     public int CultureId { get; set; }
    // public DateTime UpdatedAt { get; set; }
}
